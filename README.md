# RimWorld 机制 / 接口导航地图

想给 RimWorld 做 Mod，却不知道从哪里下手？这份地图帮助你从“想改哪个机制”找到游戏或成熟框架的代码入口、前置依赖、证据出处和还需要验证的地方，让 Mod 作者和 agent 少走弯路。

当前有 **23 个领域、193 条导航**，涉及生命周期与存档、地图生成、角色与战斗、工作与研究、生产与资源、DLC、界面与声音、调试及版本维护。这是一组选题的导航范围，不是覆盖整个游戏所有行为的承诺；它不是游戏源码，不保证零错误，也不表示所有 Mod 已兼容。

## 最短使用例

想保存自己的全局、世界或地图状态：

1. 在 [用途索引](ENTRY_INDEX.md) 找到 `A.LIFECYCLE_SAVE.components`。
2. 在 [atlas.json](atlas.json) 查同一 ID，查看 `Verse.GameComponent`、`RimWorld.Planet.WorldComponent`、`Verse.MapComponent` 的入口和 `ExposeData` 等声明。
3. 读 `recipe`、`lifecycle`、`test_approach`，确认状态归属和初始化/存档时机；不要仅凭方法名字调用内部入口。
4. 按 `applicable_version` 和目标身份，在自己的合法安装中核对签名，再做实际存读档测试。

也可以在 PowerShell 7 查询：

```powershell
$map = Get-Content ./atlas.json -Raw -Encoding utf8 | ConvertFrom-Json -Depth 100
$map.entries | Where-Object id -eq 'A.LIFECYCLE_SAVE.components'
```

## 文件怎么读

| 文件 | 用途 |
|---|---|
| [ENTRY_INDEX.md](ENTRY_INDEX.md) | 按用途快速找到导航 ID |
| [atlas.json](atlas.json) | 入口、依赖、操作思路、测试建议、状态与出处 |
| [version.json](version.json) | 目标程序集身份与本机索引统计 |
| [scripts/](scripts/) | 自有元数据索引生成器和地图结构校验器 |
| [AUDIT.md](AUDIT.md) | 公开导出的删减边界 |
| [LICENSE.md](LICENSE.md) | 文档/数据与脚本的许可范围 |

`shared_refs` 指向其他导航或领域；`evidence_refs` 指向同一 JSON 的 `sources`。公开来源保留原作者链接；本地证据位置及私有项目细节已移除，并明确标注需要自己复核。

## 版本和证据边界

原地图快照：2026-10-06。公开版日期：2026-10-06 UTC。

主要目标是 Windows `Assembly-CSharp`：

- 程序集版本：`1.6.9676.17735`
- MVID：`61e41735-6189-4da4-9d21-0260257b5097`
- SHA-256：`5cf1b5be399d5b1c9c56ca72c9d35b4ecf307feacf5859d04ac5a1aa5926356a`

这里的版本是程序集身份，不单独代替游戏显示版本。部分条目还保留 `.18020` 的历史导航边界；不能把它当作 `.17735` 的签名证明。第三方框架、DLC 和实际启用的 Mod 组合需要分别核对，没有统一兼容性保证。

原地图状态为：103 条 `ready_version_bound`、81 条 `ready_documented`、3 条 `alias`、6 条 `targeted_gap`。这些是地图记录状态，不是 184 个已通过游戏测试的功能。具体字段中 `declaration_verified` 表示记录的声明核对，`runtime_validated: false` 表示没有运行验收；详细本地证明没有在此分发。本次发布只做静态整理与索引校验，没有启动游戏。

## 在自己的合法安装中更新索引

需要 PowerShell 7。选择自己的 `Assembly-CSharp.dll`，执行：

```powershell
./scripts/build-index.ps1 -AssemblyPath '<你的游戏安装目录>/RimWorldWin64_Data/Managed/Assembly-CSharp.dll'
./scripts/validate-atlas.ps1
```

生成器以 PE 元数据读取类型和成员声明，不加载或执行目标程序集，不读取方法体。生成的 `local-index/manifest.json` 记录版本、MVID 和摘要；`signatures.jsonl` 可用于本地查找。`local-index/` 默认不提交到 Git。

版本发生变化后，用新签名核对受影响的地图入口，逐条更新版本和证据，再做行为测试。地图校验器只检查 23 域、193 个唯一 ID、证据引用和别名目标等结构，不测试游戏逻辑。不要把 DLL、完整 IL、反编译代码、游戏资产或存档上传。

发布前已在本机合法安装上运行生成器；索引的 SHA-256 与已有静态索引一致，读取前后 DLL 摘要相同。地图结构校验通过。

## 开源许可

自编说明、导航和数据组织采用 [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/)；`scripts/` 自有脚本采用 MIT。署名使用 GitHub 账号 `599424490-png`。详见 [许可范围](LICENSE.md)。游戏和第三方作者的权利不由本项目授予，第三方材料仅保留出处链接。

