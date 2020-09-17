function InsertSort(array)
	for i = 2,#array do
		local value = array[i]
		local isInsert = false
		for j=i-1,1,-1 do
			if(array[j]>value) then
				array[j+1] = array[j]
			else
				array[j+1] = value
				isInsert = true
				break
			end
		end
		if (isInsert==false) then
		array[1] = value
		end
	end
end
--array1ÅÅĞò
local array1 = {73,23,18,92}
InsertSort(array1, 1,#array1)
--array2ÅÅĞò
array2 = {73,23,18,92}
table.sort(array2)
--array1ÅÅĞòÊä³ö
print("array1ÅÅĞòÊä³ö")
for k,v in pairs(array1) do
	print(v)
end
--array2ÅÅĞòÊä³ö
print("array2ÅÅĞòÊä³ö")
for k,v in pairs(array2) do
	print(v)
end
