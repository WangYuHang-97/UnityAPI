function BubbleSort(array)
	for i = 1,#array-1 do
		for j = i+1,#array do
			if(array[i]>array[j]) then
				local temp = array[i]
				array[i] = array[j]
				array[j] = temp
			end
		end
	end
	return array
end
--array1ÅÅĞò
local array1 = {73,23,18,92}
BubbleSort(array1, 1,#array1)
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
