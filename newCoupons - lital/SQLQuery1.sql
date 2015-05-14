insert into CouponMaker

select ID=1,name='mcCoupon',[description]='best price', originalPrice=50,couponPrice=25,rating=2,numOfRaters=1,startDate='2015-04-04',endDate='2015-05-05',quantity=0,maxQuantity=100,statusID='Active',BusinessID=1

use master
alter database Coupons1
	set SINGLE_USER with rollback immediate
alter database Coupons1 SET MULTI_USER