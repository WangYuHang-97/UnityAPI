该方法用于创建以Camera为基础的地图显示,并提供移动显示方法
	1.创建新Camera,设置好想要显示的画面,Projection设置为Orthogr(正交)
	2.创建新RenderTexture(MiniMapTexture),并添加于Camera的TargetTexture中
	3.创建新RawImage(Rimg_Map),并将RenderTexture添加于Texture,此时RawImage会显示Camera中实时显示内容
	4.将MiniMapController挂载在上

注意事项:
	1.该方法在移动上数值有待更新,应该与缩放值有关