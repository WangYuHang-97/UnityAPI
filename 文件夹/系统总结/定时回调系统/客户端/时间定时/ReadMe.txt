1.	GameRootTime管理类——用于启动TimerSystem初始化	TimerSystem方法类——方法实现	PETimeTask数据类
2.	TimerSystem说明:
	--时间计时器(给定时间,时间结束时运行该方法)
	--时间计时器可以设置时间单位(毫秒,秒,分,小时,天)和循环次数
	--每个任务包含一个唯一ID,可用于删除或替换任务
3.	该方法旨用于替代协程计时方法,可大大摒除其迭代缺点