using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;

// Extends the existing PeEndpoints PEReader/MetadataReader approach.
// Never loads the target assembly and never calls GetMethodBody.
public sealed class MetadataIndex : ISignatureTypeProvider<string, object>, ICustomAttributeTypeProvider<string> {
 MetadataReader r;
 HashSet<string> allowedNames;
 bool Allowed(string name){return allowedNames==null||allowedNames.Contains(name)||((name.StartsWith("get_")||name.StartsWith("set_"))&&allowedNames.Contains(name.Substring(4)));}
 string TN(TypeDefinitionHandle h) {var t=r.GetTypeDefinition(h);var p=t.GetDeclaringType();string n=r.GetString(t.Name);string ns=r.GetString(t.Namespace);return p.IsNil?(ns.Length==0?n:ns+"."+n):TN(p)+"+"+n;}
 string TR(TypeReferenceHandle h) {var t=r.GetTypeReference(h);string n=r.GetString(t.Name);string ns=r.GetString(t.Namespace);return t.ResolutionScope.Kind==HandleKind.TypeReference?TR((TypeReferenceHandle)t.ResolutionScope)+"+"+n:(ns.Length==0?n:ns+"."+n);}
 string Ty(EntityHandle h) {if(h.IsNil)return null;switch(h.Kind){case HandleKind.TypeDefinition:return TN((TypeDefinitionHandle)h);case HandleKind.TypeReference:return TR((TypeReferenceHandle)h);case HandleKind.TypeSpecification:return r.GetTypeSpecification((TypeSpecificationHandle)h).DecodeSignature(this,null);default:return h.Kind.ToString();}}
 string Tok(EntityHandle h)=>"0x"+MetadataTokens.GetToken(h).ToString("X8");
 string Raw(BlobHandle h)=>Convert.ToHexString(r.GetBlobBytes(h));
 void Row(StreamWriter w,object value)=>w.WriteLine(JsonSerializer.Serialize(value));
 object[] GP(GenericParameterHandleCollection hs) {var list=new List<object>();foreach(var h in hs){var g=r.GetGenericParameter(h);var constraints=new List<string>();foreach(var ch in g.GetConstraints())constraints.Add(Ty(r.GetGenericParameterConstraint(ch).Type));list.Add(new {index=g.Index,name=r.GetString(g.Name),attributes=g.Attributes.ToString(),constraints});}return list.ToArray();}
 object Constant(ConstantHandle h) {if(h.IsNil)return null;var c=r.GetConstant(h);return new {typeCode=c.TypeCode.ToString(),rawHex=Raw(c.Value)};}
 object Access(MethodDefinitionHandle h)=>h.IsNil?null:new {token=Tok(h),name=r.GetString(r.GetMethodDefinition(h).Name)};
 string Sig(MethodSignature<string> s)=>s.ReturnType+" ("+string.Join(", ",s.ParameterTypes)+")";
 public static string FormatEntity(MetadataReader reader,EntityHandle h){return new MetadataIndex{r=reader}.EN(h);}
 public string GetSystemType()=>"System.Type";
 public bool IsSystemType(string t)=>t=="System.Type";
 public string GetTypeFromSerializedName(string n)=>n;
 public PrimitiveTypeCode GetUnderlyingEnumType(string t)=>PrimitiveTypeCode.Int32;
 public static string PatchAttributes(string path,string[] types){using(var fs=File.OpenRead(path))using(var pe=new PEReader(fs)){var r=pe.GetMetadataReader();var self=new MetadataIndex{r=r};var wanted=new HashSet<string>(types);var rows=new List<object>();foreach(var th in r.TypeDefinitions){string tn=self.TN(th);if(!wanted.Contains(tn))continue;var td=r.GetTypeDefinition(th);var items=new List<(string,CustomAttributeHandleCollection)>{(tn,td.GetCustomAttributes())};foreach(var mh in td.GetMethods()){var m=r.GetMethodDefinition(mh);items.Add((self.EN(mh),m.GetCustomAttributes()));}foreach(var item in items)foreach(var ah in item.Item2){var a=r.GetCustomAttribute(ah);string constructor=self.EN(a.Constructor);if(!constructor.Contains("Harmony"))continue;try{var v=a.DecodeValue(self);rows.Add(new{owner=item.Item1,constructor,fixedArguments=v.FixedArguments,namedArguments=v.NamedArguments,rawBlob=self.Raw(a.Value)});}catch(Exception e){rows.Add(new{owner=item.Item1,constructor,error=e.GetType().Name,rawBlob=self.Raw(a.Value)});}}}return JsonSerializer.Serialize(rows);}}
 public static string SelectedMetadata(string path,string[] typeNames,string[] memberNames,string output){using(var fs=File.OpenRead(path))using(var pe=new PEReader(fs)){var r=pe.GetMetadataReader();var self=new MetadataIndex{r=r};var wantedTypes=new HashSet<string>(typeNames);var wantedMembers=new HashSet<string>(memberNames);var rows=new List<object>();foreach(var th in r.TypeDefinitions){string tn=self.TN(th);if(!wantedTypes.Contains(tn))continue;var td=r.GetTypeDefinition(th);foreach(var mh in td.GetMethods()){var m=r.GetMethodDefinition(mh);string mn=r.GetString(m.Name);if(!wantedMembers.Contains(mn))continue;var sig=m.DecodeSignature(self,null);var ps=new List<object>();foreach(var ph in m.GetParameters()){var p=r.GetParameter(ph);ps.Add(new{sequence=p.SequenceNumber,name=r.GetString(p.Name),attributes=p.Attributes.ToString(),defaultValue=self.Constant(p.GetDefaultValue())});}rows.Add(new{kind="method",type=tn,name=mn,token=self.Tok(mh),attributes=m.Attributes.ToString(),signature=self.Sig(sig),returnType=sig.ReturnType,parameterTypes=sig.ParameterTypes,parameters=ps,rawSignature=self.Raw(m.Signature)});}foreach(var fh in td.GetFields()){var f=r.GetFieldDefinition(fh);string fn=r.GetString(f.Name);if(!wantedMembers.Contains(fn))continue;rows.Add(new{kind="field",type=tn,name=fn,token=self.Tok(fh),attributes=f.Attributes.ToString(),fieldType=f.DecodeSignature(self,null)});}}string json=JsonSerializer.Serialize(rows);File.WriteAllText(output,json,new UTF8Encoding(false));return json;}}
 string EN(EntityHandle h){switch(h.Kind){case HandleKind.MethodDefinition:var m=r.GetMethodDefinition((MethodDefinitionHandle)h);return TN(m.GetDeclaringType())+"::"+r.GetString(m.Name)+" "+Sig(m.DecodeSignature(this,null));case HandleKind.FieldDefinition:var f=r.GetFieldDefinition((FieldDefinitionHandle)h);return TN(f.GetDeclaringType())+"::"+r.GetString(f.Name);case HandleKind.MemberReference:var mr=r.GetMemberReference((MemberReferenceHandle)h);return EN(mr.Parent)+"::"+r.GetString(mr.Name);case HandleKind.MethodSpecification:var ms=r.GetMethodSpecification((MethodSpecificationHandle)h);return EN(ms.Method)+"<"+string.Join(",",ms.DecodeSignature(this,null))+">";case HandleKind.TypeDefinition:case HandleKind.TypeReference:case HandleKind.TypeSpecification:return Ty(h);default:return h.Kind.ToString();}}
 public static string Generate(string path,string output) {return new MetadataIndex().Run(path,output,null);}
 public static string GenerateSelected(string path,string output,string[] types) {return new MetadataIndex().Run(path,output,new HashSet<string>(types,StringComparer.Ordinal));}
 public static string GenerateSelectedNamed(string path,string output,string[] types,string[] names){return new MetadataIndex{allowedNames=new HashSet<string>(names,StringComparer.Ordinal)}.Run(path,output,new HashSet<string>(types,StringComparer.Ordinal));}
 string Run(string path,string output,HashSet<string> selected) {
 using(var fs=File.OpenRead(path))using(var pe=new PEReader(fs))using(var w=new StreamWriter(output,false,new UTF8Encoding(false))){
 r=pe.GetMetadataReader();var a=r.GetAssemblyDefinition();var mod=r.GetModuleDefinition();
 var counts=new Dictionary<string,int>{{"type",0},{"method",0},{"field",0},{"property",0},{"event",0}};
 foreach(var h in r.TypeDefinitions){var t=r.GetTypeDefinition(h);string tn=TN(h);if(selected!=null&&!selected.Contains(tn))continue;var interfaces=new List<string>();foreach(var ih in t.GetInterfaceImplementations())interfaces.Add(Ty(r.GetInterfaceImplementation(ih).Interface));
 Row(w,new {kind="type",type=tn,name=r.GetString(t.Name),token=Tok(h),attributes=t.Attributes.ToString(),baseType=Ty(t.BaseType),interfaces,genericParameters=GP(t.GetGenericParameters()),declaringType=t.GetDeclaringType().IsNil?null:TN(t.GetDeclaringType()),methodCount=t.GetMethods().Count,fieldCount=t.GetFields().Count,propertyCount=t.GetProperties().Count,eventCount=t.GetEvents().Count});counts["type"]++;
 foreach(var mh in t.GetMethods()){var m=r.GetMethodDefinition(mh);if(!Allowed(r.GetString(m.Name)))continue;var sig=m.DecodeSignature(this,null);var ps=new List<object>();foreach(var ph in m.GetParameters()){var p=r.GetParameter(ph);ps.Add(new {sequence=p.SequenceNumber,name=r.GetString(p.Name),attributes=p.Attributes.ToString(),defaultValue=Constant(p.GetDefaultValue())});}
 Row(w,new {kind="method",type=tn,name=r.GetString(m.Name),token=Tok(mh),attributes=m.Attributes.ToString(),implAttributes=m.ImplAttributes.ToString(),signature=Sig(sig),returnType=sig.ReturnType,parameterTypes=sig.ParameterTypes,signatureHeader=sig.Header.RawValue,callingConvention=sig.Header.CallingConvention.ToString(),isInstance=sig.Header.IsInstance,requiredParameterCount=sig.RequiredParameterCount,genericParameterCount=sig.GenericParameterCount,genericParameters=GP(m.GetGenericParameters()),parameters=ps,rawSignature=Raw(m.Signature)});counts["method"]++;}
 foreach(var fh in t.GetFields()){var f=r.GetFieldDefinition(fh);if(!Allowed(r.GetString(f.Name)))continue;Row(w,new {kind="field",type=tn,name=r.GetString(f.Name),token=Tok(fh),attributes=f.Attributes.ToString(),fieldType=f.DecodeSignature(this,null),defaultValue=Constant(f.GetDefaultValue()),rawSignature=Raw(f.Signature)});counts["field"]++;}
 foreach(var ph in t.GetProperties()){var p=r.GetPropertyDefinition(ph);if(!Allowed(r.GetString(p.Name)))continue;var s=p.DecodeSignature(this,null);var ac=p.GetAccessors();var others=new List<object>();foreach(var x in ac.Others)others.Add(Access(x));Row(w,new {kind="property",type=tn,name=r.GetString(p.Name),token=Tok(ph),attributes=p.Attributes.ToString(),signature=Sig(s),propertyType=s.ReturnType,parameterTypes=s.ParameterTypes,getter=Access(ac.Getter),setter=Access(ac.Setter),others,defaultValue=Constant(p.GetDefaultValue()),rawSignature=Raw(p.Signature)});counts["property"]++;}
 foreach(var eh in t.GetEvents()){var e=r.GetEventDefinition(eh);if(!Allowed(r.GetString(e.Name)))continue;var ac=e.GetAccessors();var others=new List<object>();foreach(var x in ac.Others)others.Add(Access(x));Row(w,new {kind="event",type=tn,name=r.GetString(e.Name),token=Tok(eh),attributes=e.Attributes.ToString(),eventType=Ty(e.Type),adder=Access(ac.Adder),remover=Access(ac.Remover),raiser=Access(ac.Raiser),others});counts["event"]++;}
 }
 var refs=new List<object>();foreach(var ah in r.AssemblyReferences){var ar=r.GetAssemblyReference(ah);refs.Add(new {name=r.GetString(ar.Name),version=ar.Version.ToString(),culture=r.GetString(ar.Culture),publicKeyOrToken=Raw(ar.PublicKeyOrToken),flags=ar.Flags.ToString()});}
 var tableCounts=new {type=r.GetTableRowCount(TableIndex.TypeDef),method=r.GetTableRowCount(TableIndex.MethodDef),field=r.GetTableRowCount(TableIndex.Field),property=r.GetTableRowCount(TableIndex.Property),@event=r.GetTableRowCount(TableIndex.Event)};
 return JsonSerializer.Serialize(new {name=r.GetString(a.Name),version=a.Version.ToString(),mvid=r.GetGuid(mod.Mvid),metadataVersion=r.MetadataVersion,counts,tableCounts,assemblyReferences=refs});
 }}
 public string GetArrayType(string e,ArrayShape s)=>e+"[rank="+s.Rank+";sizes="+string.Join(",",s.Sizes)+";lower="+string.Join(",",s.LowerBounds)+"]";
 public string GetByReferenceType(string e)=>e+"&";
 public string GetFunctionPointerType(MethodSignature<string> s)=>"fnptr{"+s.Header.RawValue+":"+Sig(s)+"}";
 public string GetGenericInstantiation(string g,ImmutableArray<string> a)=>g+"<"+string.Join(", ",a)+">";
 public string GetGenericMethodParameter(object c,int i)=>"!!"+i;
 public string GetGenericTypeParameter(object c,int i)=>"!"+i;
 public string GetModifiedType(string m,string u,bool required)=>u+(required?" modreq(":" modopt(")+m+")";
 public string GetPinnedType(string e)=>e+" pinned";
 public string GetPointerType(string e)=>e+"*";
 public string GetPrimitiveType(PrimitiveTypeCode c){switch(c){case PrimitiveTypeCode.Void:return "System.Void";case PrimitiveTypeCode.Boolean:return "System.Boolean";case PrimitiveTypeCode.Char:return "System.Char";case PrimitiveTypeCode.SByte:return "System.SByte";case PrimitiveTypeCode.Byte:return "System.Byte";case PrimitiveTypeCode.Int16:return "System.Int16";case PrimitiveTypeCode.UInt16:return "System.UInt16";case PrimitiveTypeCode.Int32:return "System.Int32";case PrimitiveTypeCode.UInt32:return "System.UInt32";case PrimitiveTypeCode.Int64:return "System.Int64";case PrimitiveTypeCode.UInt64:return "System.UInt64";case PrimitiveTypeCode.Single:return "System.Single";case PrimitiveTypeCode.Double:return "System.Double";case PrimitiveTypeCode.String:return "System.String";case PrimitiveTypeCode.IntPtr:return "System.IntPtr";case PrimitiveTypeCode.UIntPtr:return "System.UIntPtr";case PrimitiveTypeCode.Object:return "System.Object";case PrimitiveTypeCode.TypedReference:return "System.TypedReference";default:return c.ToString();}}
 public string GetSZArrayType(string e)=>e+"[]";
 public string GetTypeFromDefinition(MetadataReader reader,TypeDefinitionHandle h,byte raw)=>TN(h);
 public string GetTypeFromReference(MetadataReader reader,TypeReferenceHandle h,byte raw)=>TR(h);
 public string GetTypeFromSpecification(MetadataReader reader,object c,TypeSpecificationHandle h,byte raw)=>r.GetTypeSpecification(h).DecodeSignature(this,c);
}
