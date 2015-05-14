insert into CouponMaker

select ID=1,name='mcCoupon',[description]='best price', originalPrice=50,couponPrice=25,rating=2,numOfRaters=1,startDate='2015-04-04',endDate='2015-05-05',quantity=0,maxQuantity=100,statusID='Active',BusinessID=1

use master
alter database Coupons1
	set SINGLE_USER with rollback immediate
alter database Coupons1 SET MULTI_USER


        <div class="form-group">
            @Html.LabelFor(model => model.Status, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EnumDropDownListFor(model => model.Status, htmlAttributes: new { @class = "form-control" })
                @Html.ValidationMessageFor(model => model.Status, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.ID, "CouponMakerID", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("CouponMakerID", null, htmlAttributes: new { @class = "form-control" })
                @Html.ValidationMessageFor(model => model.CouponMakerID, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.CustomerID, "CustomerID", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("CustomerID", null, htmlAttributes: new { @class = "form-control" })
                @Html.ValidationMessageFor(model => model.CustomerID, "", new { @class = "text-danger" })
            </div>
        </div>