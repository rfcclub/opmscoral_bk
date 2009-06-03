﻿<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
<form id="mainform">
<h2>Thông tin Phiếu yêu cầu</h2>
<table border="0" width="500px" cellspacing="1" cellpadding="1" id="table1">
	<tr>
		<td nowrap><label>Mã phiếu&nbsp;</label></td>
		<td>
			<input type="text" name="txtRequestId" size="20px" maxlength="20" disabled="disabled">
		</td>
		<td nowrap><label>Ngày&nbsp;</label></td>
		<td>
			<input type="text" name="txtRequestDate" size="20px" maxlength="10" readonly="readonly">
		</td>
	</tr>
	<tr>
		<td nowrap ><label>Mã công ty&nbsp;</label></td>
		<td>
			<input type="text" name="txtCompanyId" size="20px" maxlength="10"><input type="button" value="..." >&nbsp;
		</td>
		<td nowrap ><label>Mã khách hàng&nbsp;</label></td>
		<td>
			<input type="text" name="txtCustomerId" size="20px" maxlength="10"><input type="button" value="..." >
		</td>
	</tr>
	<tr>
		<td nowrap ><label>Tên công ty&nbsp;</label></td>
		<td>
			<input type="text" name="txtCompanyName" size="20px" maxlength="100">
		</td>
		<td nowrap ><label>Tên khách hàng&nbsp;</label></td>
		<td>
			<input type="text" name="txtCustomerName" size="20px" maxlength="100">
		</td>
	</tr>
	<tr>
		<td nowrap ><label>Địa chỉ&nbsp;</label></td>
		<td colspan="3">
			<input type="text" name="txtAddress" size="72px" maxlength="500">
		</td>
	</tr>
	<tr>
		<td nowrap ><label>Điện thoại&nbsp;</label></td>
		<td>
			<input type="text" name="txtPhone" size="20px" maxlength="30">
		</td>
		<td nowrap ><label>Fax&nbsp;</label></td>
		<td>
			<input type="text" name="txtFax" size="20px" maxlength="30">
		</td>
	</tr>
	<tr>
		<td nowrap ><label>Loại máy&nbsp;</label></td>
		<td>
			<select name="cbbMachineType">
                    <option value="UNIVERSAL_RECORD">Universal Record</option>
                    <option value="VEHICLE">Vehicle Reservation</option>
                    <option value="MCO">MCO Number</option>
                    <option value="ELECTRONIC_TICKET">Electronic Ticket</option>
            </select>
		</td>
		<td nowrap ><label>Số máy&nbsp;</label></td>
		<td>
			<input type="text" name="txtFax" size="20px" maxlength="30">
		</td>
	</tr>
	<tr>
		<td nowrap  valign="top"><label>Mô tả&nbsp;</label></td>
		<td colspan="3">
			<textarea name="txtDescription" rows="6" cols="60"></textarea>
		</td>
	</tr>
	<tr>
		<td colspan="4" align="center">
			<input type="button" value="Lưu" >
			<input type="button" value="Bỏ qua">
		</td>
	</tr>
</table>
</form>
</body>
</html>