$('#save').click(function () {
    $.ajax({
        url: '/api/Customer/AddCustomer',
        type: 'POST',
        data: {
            customerTitleName: title,
            customerFName: fname,
            customerLName: lname,
            customerPhone: phone,
            customerAddress: address,
            customerImage: image,
            provinceCode: provinceCode,
            districtCode: districtCode,
            subdistrictCode: subdistrictCode
        },
        success: function (data) {
            console.log(data);
        },
        error: function (xhr, status, error) {
            console.error('Error creating customer: ' + error);
        }
    });