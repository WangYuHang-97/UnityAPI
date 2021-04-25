1.	GameRootFrame管理类——用于启动FrameSystem初始化	FrameSystem方法类——方法实现	PEFrameTask数据类
2.	FrameSystem说明:
	--时间计时器(给定时间,时间结束时运行该方法)
	--时间计时器可以设置循环次数
	--每个任务包含一个唯一ID,可用于删除或替换任务
3.	该方法旨用于替代协程计时方法,可大大摒除其迭代缺点