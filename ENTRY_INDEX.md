# 用途索引

23 个领域、193 条导航；不是全部游戏行为的覆盖承诺。

## A.ANIMALS_ECOLOGY

- `AC.TRAINING_TAMING`：驯养、训练和动物指令沿用原生能力状态（ready_documented）
- `AC.BODY_PRODUCTS`：产奶、剪毛沿用动物资源组件和原生采集劳动（ready_documented）
- `AC.EGGS_HATCH`：用真实蛋与孵化组件生成新动物，保留受精和温度状态（ready_documented）
- `AC.WILDLIFE_POPULATION`：让野生动物数量随地图生态由游戏自然维护（ready_documented）
- `AC.PENS_GRAZING`：围栏、牵引和牧草承载沿用地图原生判断（ready_documented）
- `AC.PLANT_LIFECYCLE`：作物按真实肥力、温度、光照和日夜条件成长，再由真实劳动播收（ready_version_bound）
- `AC.WEATHER_SEASONS`：读取真实天气、季节和风雨条件给农业与能源评估（ready_documented）
- `AC.FISHING_WATERBODY`：把捕鱼接到真实水域种群与捕鱼区，而不是无限产鱼（ready_documented）

## A.AUDIO_ASSETS

- `A1.SOUND_DEF`：声明一次音效或持续声音，并找到正确音频资源。（ready_version_bound）
- `A2.ONESHOT`：在一次确认、事件或地图动作发生时播放一次声音。（ready_version_bound）
- `A3.SUSTAINER`：为持续机器或天气维持声音，并在自有活动结束时停止。（targeted_gap）
- `A4.PARAMETER_MIX`：让持续声音随速度、距离或项目数值变化。（ready_version_bound）
- `A5.MUSIC`：在进入游戏或特殊事件时使用已有音乐管理，而不是另建播放器。（ready_version_bound）
- `A6.AMBIENCE`：切换地图或归档恢复后，环境声音跟着当前地图更新。（targeted_gap）

## A.BOOTSTRAP_DEBUG

- `A.BOOTSTRAP_DEBUG.MANAGED_BOOT_CONTEXT`：把初始化放在游戏准备好的时点（alias）
- `A.BOOTSTRAP_DEBUG.DEBUG_COMMAND_REGISTRY`：直接使用原版和Mod的开发菜单命令（ready_version_bound）
- `A.BOOTSTRAP_DEBUG.THREAD_DISPATCH`：让工具在正确线程读写游戏状态（ready_documented）
- `A.BOOTSTRAP_DEBUG.EXTERNAL_BOOTSTRAP`：必要时附加C#调试器，不自造注入器（ready_documented）
- `A.BOOTSTRAP_DEBUG.DEBUG_FLAGS`：避免开发开关让业务或性能结论失真（ready_version_bound）

## A.BUILDING_ENVIRONMENT

- `AB.CONSTRUCTION`：由蓝图送入真实材料、完成建筑，并按原有材料处理取消（ready_version_bound）
- `AB.BUILDING_LIFECYCLE`：建筑放置、搬走、重装和拆毁都保留正确组件与原物（ready_documented）
- `AB.ROOM_REGIONS_ROOFS`：查询真实房间、屋顶和结构变化，而不是缓存一份过时房间（ready_documented）
- `AB.POWER_TOPOLOGY`：电线和设备交给原生建网，项目只读当前连通关系（ready_documented）
- `AB.POWER_ENERGY`：从真实供给和应有需求计算项目电力档位，不让断电负载从分母消失（ready_version_bound）
- `AB.THERMAL`：室内温度、冰箱和热量输入直接走原生或已选冷冻设施（ready_version_bound）
- `AB.FUEL_COST`：沿用燃料组件和现有热量账，只补缺少的Local燃料来源证明（ready_documented）
- `AB.FIRE`：需要火灾或灭火时复用原生火焰与工作流程（ready_documented）
- `AB.TERRAIN_LAYERS`：铺地、挖地和农田肥力按1.6多层地形接口修改（ready_documented）
- `AB.LIGHT_GRID`：让灯具发光、植物读取地面光照，沿用1.6光网格（ready_documented）
- `AB.MAINTENANCE`：设备坏了由原生维修，只限制项目允许的材料来源（ready_version_bound）
- `AB.FILTH`：普通污物生成和清洁用原生流程，堆肥原料另按物品处理（ready_documented）
- `AB.FLUID_NETWORKS`：设备用真实管网取水或资源，物品与管网只在交接边记账（targeted_gap）
- `AB.ATMOSPHERE`：房间失压、真空暴露和设备真空限制复用Odyssey原生机制（alias）

## A.COMBAT_AI

- `A.COMBAT.VERBS`：配置武器攻击、近战或特殊射击动作（ready_documented）
- `A.COMBAT.ABILITIES`：给人物增加有目标、冷却和多个效果的技能（ready_documented）
- `A.COMBAT.TARGETING`：选中玩家施法目标或让AI挑可攻击敌人（ready_documented）
- `A.COMBAT.PROJECTILES`：让弹体飞行、碰撞、爆炸或产生命中效果（ready_documented）
- `A.COMBAT.DAMAGE`：造成伤害、设置护甲穿透或定义新伤害类型（ready_documented）
- `A.COMBAT.DRAFT_AI`：征召人物并使用原生战斗决策（ready_documented）
- `A.COMBAT.GROUP_AI`：让一群人物集结、防守、进攻、撤退或完成群体仪式（ready_documented）

## A.DEFS_ECONOMY

- `AE.DEF_EFFECTIVE`：给物品、设备、配方和材料建立能正常加载的内容入口（ready_documented）
- `AE.THING_CUSTODY`：让原物在地图、搬运者和容器间交接，避免复制、丢失或双扣（ready_version_bound）
- `AE.MATERIAL_PRODUCT`：制作时沿用材料、品质和产物组件，不把复杂产品降成一串数量（ready_documented）
- `AE.STORAGE_FILTER`：把仓格、容器容量与Local/物流仓身份分别接到正确层（ready_documented）
- `AE.BILL_RECIPE`：普通配方直接走工作单，只补项目要求的Local取料与产出回执（ready_version_bound）
- `AE.UNFINISHED_WORK`：工作暂停或换人后继续同一个已付料半成品（ready_version_bound）
- `AE.FINITE_RESOURCES`：采矿或深钻时从真实有限资源扣减，而不是无限凭空出货（ready_documented）
- `AE.PERISHABLES`：让食物在仓库、途中和封存后仍保留同一份鲜度（ready_version_bound）
- `AE.AUTOMATIC_PROCESSORS`：让发酵、堆肥或自动加工设备直接使用成熟批次处理器（ready_documented）
- `AE.MAP_LOGISTICS`：沿用原生搬运，把计划数量与真实到仓分开（ready_documented）

## A.DLC_ANOMALY

- `A.DLC_ANOMALY.GLOBAL_PROGRESSION`：接入异象巨石、全局进度与实体图鉴（ready_version_bound）
- `A.DLC_ANOMALY.STUDY_KNOWLEDGE`：研究被收容实体，积累异象独立知识（ready_version_bound）
- `A.DLC_ANOMALY.STUDY_UNLOCKS`：让实体研究到一定阶段时解锁说明或触发一个效果（ready_version_bound）
- `A.DLC_ANOMALY.CONTAINMENT`：把真实实体送上收容台、读取收容强度或释放它（ready_version_bound）
- `A.DLC_ANOMALY.ACTIVITY_ESCAPE`：增加抑制或活动变化，同时保留原生逃脱规则（ready_version_bound）
- `A.DLC_ANOMALY.PSYCHIC_RITUAL`：增加异象心灵仪式的角色、阶段和结果（ready_version_bound）
- `A.DLC_ANOMALY.MUTANTS`：保留变异体的转换、恢复、能力和装备处理（ready_version_bound）
- `A.DLC_ANOMALY.CREEP_JOINERS`：组合神秘加入者的外形、好处、隐患与拒绝后果（ready_version_bound）
- `A.DLC_ANOMALY.METALHORROR`：读取金属恐魔感染来源，并接入检查或爆发剧情（ready_version_bound）
- `A.DLC_ANOMALY.UNNATURAL_CORPSE`：让异常尸体、被纠缠者和唤醒实体保持关联（ready_version_bound）
- `A.DLC_ANOMALY.ANOMALOUS_STRUCTURES`：给虚空结构或方尖碑提供调查、抑制和触发交互（ready_version_bound）
- `A.DLC_ANOMALY.PIT_GATE`：扩展深坑传送入口及其中的遭遇（ready_version_bound）
- `A.DLC_ANOMALY.FLESHMASS`：添加血肉块增长源或与心脏、卷须联动的效果（ready_version_bound）

## A.DLC_BIOTECH

- `A.BIOTECH.GENES`：增加基因、异种人或基因驱动的能力/健康效果（ready_documented）
- `A.BIOTECH.GENE_PRODUCTS`：抽取基因、保存基因包并组装异种基因（ready_documented）
- `A.BIOTECH.REPRODUCTION`：处理妊娠、出生结果和新生儿父母关系（ready_version_bound）
- `A.BIOTECH.LEARNING`：让儿童寻找学习活动、找老师并获得成长点（ready_version_bound）
- `A.BIOTECH.CHILDCARE`：给婴儿喂奶、搬到安全处并安排照护活动（ready_version_bound）
- `A.BIOTECH.GROWTH_VAT`：让胚胎或人物进入成长舱并推进生长（ready_documented）
- `A.BIOTECH.MECH_CONTROL`：分配机械体控制组、带宽和工作模式（ready_documented）
- `A.BIOTECH.MECH_GESTATION`：在孕育器中制造、修复或复活机械体（ready_documented）
- `A.BIOTECH.POLLUTION`：污染或清理地图格子并让环境效果生效（ready_documented）

## A.DLC_IDEOLOGY

- `A.DLC_IDEOLOGY.REGISTRY_FOUNDATION`：创建一套信仰体系，并把成员指向同一份信仰（ready_version_bound）
- `A.DLC_IDEOLOGY.PRECEPT_COMPOSITION`：用模因、戒律和少量组件组合一种信仰行为（ready_version_bound）
- `A.DLC_IDEOLOGY.MEMBERSHIP_CONVERSION`：改变一个人的信仰，或按原生规则推动改宗（ready_version_bound）
- `A.DLC_IDEOLOGY.SOCIAL_ROLES`：分配信仰领袖、道德导师和专家角色（ready_version_bound）
- `A.DLC_IDEOLOGY.RITUAL_OBLIGATIONS`：提供节日或义务仪式入口，并保留重复惩罚与日期条件（ready_version_bound）
- `A.DLC_IDEOLOGY.RITUAL_EXECUTION`：让一组人按角色和阶段完成一次仪式（ready_version_bound）
- `A.DLC_IDEOLOGY.RITUAL_OUTCOMES`：根据仪式质量给予思想、奖励或自定义结果（ready_version_bound）
- `A.DLC_IDEOLOGY.HISTORY_EVENTS`：记录一件行为，让信仰规则或历史统计响应（ready_version_bound）
- `A.DLC_IDEOLOGY.FLUID_REFORMATION`：让流变信仰累计发展点并改革（ready_version_bound）
- `A.DLC_IDEOLOGY.RELICS`：生成同一件圣物，并让线索任务与展示容器引用它（ready_version_bound）
- `A.DLC_IDEOLOGY.ARCHONEXUS`：接到超凡枢纽的阶段任务与终局倒计时（ready_version_bound）
- `A.DLC_IDEOLOGY.TREE_BONDS`：连接高兰树、修剪并切换树精生产模式（ready_version_bound）
- `A.DLC_IDEOLOGY.DRYAD_HOLDERS`：让树精进入茧或治疗舱，完成转换/治疗后退出（ready_version_bound）

## A.DLC_ODYSSEY

- `A.DLC_ODYSSEY.capture_ship`：把飞船原有建筑、人物和货物整体移到另一张地图（targeted_gap）
- `A.DLC_ODYSSEY.boarding`：让角色走上或离开飞船，保留原生寻路/失败规则（ready_version_bound）
- `A.DLC_ODYSSEY.launch`：起飞前检查燃料、设备与乘员，再由原生完成起飞（targeted_gap）
- `A.DLC_ODYSSEY.orbit`：飞船在地表和轨道之间正确旅行（ready_version_bound）
- `A.DLC_ODYSSEY.landing`：选落点并把原船载荷落回地图（targeted_gap）
- `A.DLC_ODYSSEY.vacuum`：轨道建筑继续使用原生真空和供氧规则（ready_version_bound）
- `A.DLC_ODYSSEY.landmarks`：生成或发现地标、轨道信号和探索地点（ready_documented）
- `A.DLC_ODYSSEY.shuttle_bridges`：飞船仪式、穿梭机与任务保持各自原生规则（ready_version_bound）

## A.DLC_ROYALTY

- `A.DLC_ROYALTY.TITLES_FAVOR`：授予荣誉、晋爵、设置继承人或读取爵位资格（ready_version_bound）
- `A.DLC_ROYALTY.PERMITS`：给爵位提供征召援军、物资投送等许可（ready_version_bound）
- `A.DLC_ROYALTY.ENTROPY_PSYFOCUS`：读取或改变超能专用的灵能热量、专注度与目标值（ready_version_bound）
- `A.DLC_ROYALTY.MEDITATION_FOCUS`：增加一种可用冥想焦点或分配冥想点（ready_version_bound）
- `A.DLC_ROYALTY.PSYCAST_DELTA`：增加一种使用灵能热量和专注度的超能力（ready_version_bound）
- `A.DLC_ROYALTY.TECHPRINTS`：保留蓝图实物的取得与应用，作为科技购买资格（ready_version_bound）

## A.DYNAMIC_PATCH

- `A.DYNAMIC_PATCH.ORDER`：在已有功能前后接自己的小段逻辑（ready_documented）
- `A.DYNAMIC_PATCH.TRANSPILE`：只有必须改方法内部时才使用更脆弱的接法（ready_documented）
- `A.DYNAMIC_PATCH.FAILURE`：操作抛错时不要误以为尾部逻辑一定执行（ready_documented）
- `A.DYNAMIC_PATCH.ACTIVE_CHAIN`：查清某个冲突到底是哪几个Mod碰了同一入口（ready_documented）

## A.FACTIONS_TRADE_TRAVEL

- `A.FACTIONS_TRADE_TRAVEL.factions`：创建和查找派系，同时保留存档身份（ready_documented）
- `A.FACTIONS_TRADE_TRAVEL.relations`：用原生外交接口改变好感，避免漏掉通知副作用（ready_version_bound）
- `A.FACTIONS_TRADE_TRAVEL.trade_source`：为商人、商船或城镇提供可买卖商品（ready_version_bound）
- `A.FACTIONS_TRADE_TRAVEL.trade_transaction`：让买卖价格、付款和实物交接仍按游戏原规则完成（ready_version_bound）
- `A.FACTIONS_TRADE_TRAVEL.formation`：组成、重新组队、拆分或合并真实远行队（ready_version_bound）
- `A.FACTIONS_TRADE_TRAVEL.travel`：让远行队按原生路线移动与停靠，而不是用后台数字伪造到达（ready_version_bound）
- `A.FACTIONS_TRADE_TRAVEL.arrival`：真实人物抵达后进入原城，避免只开地图就算接管（ready_version_bound）
- `A.FACTIONS_TRADE_TRAVEL.travel_bridges`：旅行中的吃饭、休息、任务和贸易事件继续交给对应原生系统（ready_documented）

## A.LIFECYCLE_SAVE

- `A.LIFECYCLE_SAVE.bootstrap`：模组加载后安全安装自己的功能入口（ready_version_bound）
- `A.LIFECYCLE_SAVE.components`：把每局、世界和地图数据放在正确容器，并接原生更新与存档（ready_version_bound）
- `A.LIFECYCLE_SAVE.new_load_exit`：新建、普通读档、封存原城和回城使用正确现成路径（ready_version_bound）
- `A.LIFECYCLE_SAVE.long_events`：把地图转换放进原生加载队列，并在正确时间继续（ready_version_bound）
- `A.LIFECYCLE_SAVE.field_serialize`：保存数值、定义、对象和容器，读档后恢复业务字段（ready_version_bound）
- `A.LIFECYCLE_SAVE.references`：保持原城市、人物和货物身份，避免恢复出复制品（ready_version_bound）
- `A.LIFECYCLE_SAVE.save_failures`：失败时保留可恢复原城，普通保存不把坏账本写入（ready_version_bound）
- `A.LIFECYCLE_SAVE.migration`：升级格式时明确旧存档怎么读（alias）

## A.PAWN_BIOLOGY_SOCIAL

- `A.PAWN.IDENTITY`：生成一个新人物，或移动并保住现有原人身份（ready_documented）
- `A.PAWN.COMPONENTS`：给合适种族或生命阶段的人物挂附加状态（ready_version_bound）
- `A.PAWN.PERSONAL_ITEMS`：装备武器、穿衣、收纳随身物品并保留所有权（ready_documented）
- `A.PAWN.NEEDS`：增加饥饿、休息、心情等需求并按游戏更新（ready_version_bound）
- `A.PAWN.THOUGHTS`：让住房、社交或事件产生即时心情和记忆（ready_documented）
- `A.PAWN.MENTAL`：触发并结束精神状态或增加一种崩溃行为（ready_documented）
- `A.PAWN.HEALTH_SET`：施加、移除伤病和检查伤病导致的倒地死亡（ready_documented）
- `A.PAWN.HEDIFF_COMPS`：给伤病增加周期效果、治疗通知或自定义存档字段（ready_version_bound）
- `A.PAWN.HEDIFF_GIVERS`：按种族、条件或周期自动施加伤病（ready_documented）
- `A.PAWN.ANATOMY_CAPACITY`：检查人物能否搬运、工作、行动或承受某部位效果（ready_documented）
- `A.PAWN.TENDING`：让医生拿药并真正治疗伤病（ready_version_bound）
- `A.PAWN.SURGERY`：提供可排队的手术、安装义肢或移除部位（ready_documented）
- `A.PAWN.DEATH`：正确死亡、生成尸体或复活同一个人物（ready_documented）
- `A.PAWN.SKILLS`：按正常劳动学习技能，或投射已结算的岗位经验（ready_documented）
- `A.PAWN.STORY_TRAITS`：设置背景、特性与工作禁用规则（ready_documented）
- `A.PAWN.RELATIONS`：添加亲属、婚姻等关系并查询意见（ready_version_bound）
- `A.PAWN.INTERACTIONS`：新增聊天、安抚、招募等社交互动（ready_documented）
- `A.PAWN.GUEST_STATUS`：让人物成为访客、囚犯或奴隶并设置互动方式（ready_documented）
- `A.PAWN.AGE`：推进生理年龄并触发生命阶段和成长（ready_version_bound）
- `A.PAWN.ASSIGNMENT`：把人物分给床位或可分配设施并打开原生选人窗口（ready_version_bound）

## A.PROFILE_PROVENANCE

- `P1.IDENTITY_ORDER`：知道这次构建配置了哪些依赖、顺序如何，以及哪些还没有加载证据。（ready_documented）
- `P2.VERSION_CONTENT`：让1.6只读取正确版本、平台及依赖条件下的资源。（ready_documented）
- `P3.OPTIONAL_ADAPTER`：兼容外部框架时，明确可用能力并安全处理缺包或API变化。（ready_version_bound）
- `P4.RIGHTS_LINEAGE`：知道代码、接口说明和美术从哪里来，哪份可用，哪份缺原件。（ready_documented）

## A.QUEST_EVENTS

- `A.QUEST_EVENTS.GENERATION`：做一条可接取的任务，或一组前后关联任务（ready_version_bound）
- `A.QUEST_EVENTS.RUNTIME_PARTS`：让任务接受、收到条件信号、结束并清理（ready_version_bound）
- `A.QUEST_EVENTS.ACTIVATABLE_DELAY`：等若干游戏 tick，再推进任务或使一个任务阶段到期（ready_version_bound）
- `A.QUEST_EVENTS.SIGNALS`：让任务和事件通知彼此，不给每个模组另写消息总线（ready_version_bound）
- `A.QUEST_EVENTS.CHOICES_REWARDS`：在任务中提供几种奖励，让玩家选择并发放（ready_version_bound）
- `A.QUEST_EVENTS.STORYTELLER`：组合一个叙事者，让它按时提出事件（ready_version_bound）
- `A.QUEST_EVENTS.INCIDENT_WORKERS`：增加一种事件，并让游戏判断是否能发生（ready_version_bound）
- `A.QUEST_EVENTS.INCIDENT_QUEUE`：安排一个事件在游戏里的某时刻发生（ready_version_bound）

## A.RENDER_UI

- `R1.ASSET_RESOLVE`：把纹理、图标、音频和自定义shader接到正确资源。（ready_documented）
- `R2.GRAPHIC_MATERIAL`：让建筑、作物和物品使用现成单向/多向图形与正确尺寸。（ready_version_bound）
- `R3.MAP_MESH`：更新地形/建筑后只重建必要的地图网格。（ready_version_bound）
- `非公开项目或证据位置已移除；此项需在自己的目标版本重新核对。`：让需要逐帧变化的物体跟随地图正确出现和消失。（ready_version_bound）
- `R5.PAWN_RENDER`：为居民添加外观、装备或入住隐藏，同时保留原人身份。（ready_version_bound）
- `R6.WORLD_DRAW`：在地表、轨道等不同世界层画地点，并正确选中地点。（ready_documented）
- `R7.WINDOW_STACK`：打开聚落窗口、确认框或弹出菜单，并正确处理关闭。（ready_documented）
- `R8.WIDGET_UNITS`：在固定封稿上保持文字、滚动、按钮和提示区域一致。（ready_documented）
- `R9.TABS_INSPECTION`：把研究、建筑和居民详情放进正确页签，避免操作失效选择。（ready_version_bound）
- `R10.COMMAND_TARGET`：给玩家按钮或地图/世界目标选择，并只在目标有效时提交动作。（ready_version_bound）
- `R11.CONTEXT_PROVIDER`：给右键菜单增加一项，让其他菜单项继续正常工作。（ready_documented）
- `R12.DESIGNATOR_STYLE`：复用原生拖拽划区和放置预览，让施工/挖土仍由正常任务执行。（ready_documented）

## A.TEST_EXECUTION

- `A.TEST_EXECUTION.CAPABILITY_CONTRACT`：找得到现成工具，并按正确参数调用项目能力（ready_version_bound）
- `A.TEST_EXECUTION.SCHEDULER_PROGRESS`：把准备、推进、观察串成有边界的测试步骤（ready_version_bound）
- `A.TEST_EXECUTION.FIXTURE_ISOLATION`：用可重复的测试存档验证业务，而不污染原存档（ready_version_bound）
- `A.TEST_EXECUTION.ORACLE_RESULTS`：分清工具成功、业务通过和未完成（ready_documented）
- `A.TEST_EXECUTION.LOG_OBSERVABILITY`：失败时取得能定位步骤的日志和事实（ready_documented）
- `A.TEST_EXECUTION.CI_PROCESS_LIFETIME`：让自动化正确收尾，并让报告反映真实完成程度（ready_documented）

## A.TYPE_MAP

- `A.TYPE_MAP.IDENTITY`：让代码生成与编译针对同一个实际版本（ready_version_bound）
- `A.TYPE_MAP.INHERITANCE`：找到一个功能应该继承什么、加哪类组件（ready_version_bound）
- `A.TYPE_MAP.ENTRY_INDEX`：从“我要做什么”直接找到代码入口和成熟例子（ready_documented）
- `A.TYPE_MAP.REVIEW`：维护一份不会混淆“已知接口”和“已测功能”的地图（ready_documented）

## A.UPGRADE_MAINTENANCE

- `A.UPGRADE_MAINTENANCE.ABI_IMPACT`：升级游戏或框架时只重查真正受影响的接点（ready_documented）
- `A.UPGRADE_MAINTENANCE.DEF_CONVERTERS`：让旧存档中的改名Def、类型或枚举有明确去向（ready_version_bound）
- `A.UPGRADE_MAINTENANCE.SAVE_VERSION_POLICY`：升级前看清存档版本和mod集合是否匹配（ready_version_bound）
- `A.UPGRADE_MAINTENANCE.CACHE_RELOAD`：区分能热替换的方法与必须重启的数据变化（ready_documented）
- `A.UPGRADE_MAINTENANCE.EVIDENCE_EXPIRY`：以后换版本时知道哪些结论还能用（ready_documented）

## A.WORK_TIME_AI

- `A.WORK.WORK_SELECTION`：给人物提出一类工作，并遵守原生优先级和资格（ready_documented）
- `A.WORK.THINK_TREE`：组合人物的工作、休息、战斗和特殊AI选择（ready_documented）
- `A.WORK.JOB_LIFETIME`：实现一个可预约、移动、等待、完成和中断的真实任务（ready_version_bound）
- `A.WORK.RESERVATION`：防止多人争抢同一物体、工作台、床位或数量（ready_documented）
- `A.WORK.PATHING`：让人物走到目标，或只查询一条可达路径（ready_version_bound）
- `A.WORK.CARRY_HOLDER`：拿起、搬运、放下物品或转交人物持有者（ready_version_bound）
- `A.WORK.TIMETABLE`：按玩家时间表安排工作/休息，并正确读取游戏时间（ready_documented）

## A.WORLD_MAPGEN

- `A.WORLD_MAPGEN.world_layers`：让城市与运输正确识别地表、轨道及其它地图层（ready_documented）
- `A.WORLD_MAPGEN.world_steps`：添加世界层生成或游戏全局准备步骤（ready_documented）
- `A.WORLD_MAPGEN.map_steps`：在新地图生成时按顺序添加区域与建筑（ready_version_bound）
- `A.WORLD_MAPGEN.terrain_biome`：修改地形、屋顶和迷雾时保留原生地图规则（ready_documented）
- `A.WORLD_MAPGEN.map_parent`：新增城市世界对象并正确关联真实地图（ready_version_bound）
- `A.WORLD_MAPGEN.layout_scatter`：把建筑布局、房间或预制件放进新地图或施工图（ready_version_bound）
- `A.WORLD_MAPGEN.cancel_restore`：生成新城失败与恢复旧城失败分别保全正确对象（ready_version_bound）
- `A.WORLD_MAPGEN.framework_layout`：直接用成熟结构框架生成新聚落布局（ready_documented）
