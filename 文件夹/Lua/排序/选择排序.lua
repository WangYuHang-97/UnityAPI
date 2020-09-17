function ChoiceSort(array)
	for i=1,#array -1 do
		local min = array[i]
		local minIndex = i
		for j=i+1,#array do
			if(array[j]<min) then
				minIndex = j
			end
		end
		if(minIndex ~= min) then
			local temp = array[minIndex]
			array[minIndex] = array[i]
			array[i] = temp
		end
	end
	return array
end
--array1ÅÅÐò
local array1 = {73,23,18,92}
ChoiceSort(array1, 1,#array1)
--array2ÅÅÐò
array2 = {73,23,18,92}
table.sort(array2)
--array1ÅÅÐòÊä³ö
print("array1ÅÅÐòÊä³ö")
for k,v in pairs(array1) do
	print(v)
end
--array2ÅÅÐòÊä³ö
print("array2ÅÅÐòÊä³ö")
for k,v in pairs(array2) do
	print(v)
end

