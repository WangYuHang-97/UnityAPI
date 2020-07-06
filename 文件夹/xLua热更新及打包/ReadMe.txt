1.	从https://github.com/Tencent/xLua/releases,导入xLua插件放置于Assets目录,此时Unity菜单栏会显示Xlua,其中包含Generate Code、Clear Generate Code
2.	设置unity:File => BuildSettings => PlayerSettings => OtherSettings => Configuration => Scripting Define Symbols 中添加 HOTFIX_ENABLE,此时Xlua中会添加Hotfix inject in Editor
3.	将CreateAssetBundles脚本添加至Assets目录的Editor文件夹中,此时Assets栏中会增加Build AssetBundles,用于打包
4.  添加lua文件,包括若干补丁更新文件(可为1),及Dispose.lua(所有补丁更新文件必须在此文件夹Dispose)和util.lua(util方法调用)
5.  指定物体添加HotFixScript脚本,在所有脚本之前调用该脚本
6.  添加HotFixEmpty脚本于资源文件,该脚本用于预防可能增加新功能
7.	在lua补丁文件中添加方法即可实现热更新