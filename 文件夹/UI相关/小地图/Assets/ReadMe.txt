该方法用于创建以Camera为基础的地图显示,并提供移动显示方法
	1.创建新Camera,设置好想要显示的画面,Projection设置为Orthogr(正交)
	2.创建新RenderTexture(MiniMapTexture),并添加于Camera的TargetTexture中
	3.创建新RawImage,在子物体中创建新Button(initButton)置于合适位置,并将RenderTexture添加于Texture,此时RawImage会显示Camera中实时显示内容
	4.将MiniMap挂载在Img_MiniMap上

