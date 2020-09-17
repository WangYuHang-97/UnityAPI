function BinarySearch1(array,n)
	local left = 0
	local right = #array+1
	while(right - left ~= 1) do
		local mid = math.floor(left + (right - left)/2)
		if(n<array[mid]) then
			right = mid
		elseif(n>array[mid]) then
			left = mid
		else
			return mid
		end
	end
	return -1
end
array = {1,2,3,4,5}
print(BinarySearch1(array,1))

