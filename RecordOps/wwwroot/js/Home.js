$(document).ready(function () {
   
    // เมื่อ dropdown จังหวัด ถูกเปลี่ยนค่า
    $('#province-dropdown').change(function () {
        // รับค่าจาก option ที่ถูกเลือก
        var selectedProvince = $(this).val();

        if (selectedProvince) {
            // ส่งค่าไปยัง Controller เพื่อดึงข้อมูลเขต (District)
            $.ajax({
                url: '/Home/GetDistrict',  // เปลี่ยนเป็น URL ของคุณ
                type: 'GET',
                data: { provinceCode: selectedProvince },  // ส่งค่า provinceId
                success: function (districts) {
                    console.log(districts);
                    // เคลียร์ข้อมูลเดิมใน dropdown ของ District
                    $('#district-dropdown').empty();
                    $('#district-dropdown').append('<option value="">-- กรุณาเลือกอำเภอ --</option>');

                    // เติมข้อมูลที่ได้จาก Controller ลงใน dropdown
                    $.each(districts, function (index, district) {
                        $('#district-dropdown').append('<option value="' + district.value + '">' + district.text + '</option>');
                    });
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching districts: ' + error);
                }
            });

            // ส่งค่าไปยัง Controller เพื่อดึงข้อมูลตำบล (Subdistrict)
            $.ajax({
                url: '/Home/Getsubdistrict',  // เปลี่ยนเป็น URL ของคุณ
                type: 'GET',
                data: { provinceCode: selectedProvince },
                success: function (subdistricts) {
                    //console.log(subdistricts);
                    // เคลียร์ข้อมูลเดิมใน dropdown ของ Subdistrict
                    $('#subdistrict-dropdown').empty();
                    $('#subdistrict-dropdown').append('<option value="">-- กรุณาเลือกตำบล --</option>');

                    // เติมข้อมูลที่ได้จาก Controller ลงใน dropdown
                    $.each(subdistricts, function (index, subdistrict) {
                        //console.log(subdistrict.Text);
                        $('#subdistrict-dropdown').append('<option value="' + subdistrict.value + '">' + subdistrict.text + '</option>');
                    });
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching subdistricts: ' + error);
                }
            });
        } else {
            // ถ้าไม่เลือกจังหวัด ให้ล้างข้อมูลใน dropdown
            $('#district-dropdown').empty().append('<option value="">-- กรุณาเลือกอำเภอ --</option>');
            $('#subdistrict-dropdown').empty().append('<option value="">-- กรุณาเลือกตำบล --</option>');
        }
    });
    $('#district-dropdown').change(function () {
        var selectedDistrict = $(this).val();
        if (selectedDistrict) {
            $.ajax({
                url: '/Home/Getsubdistrict',
                type: 'GET',
                data: { districtCode: selectedDistrict },
                success: function (subdistricts) {
                    console.log(subdistricts);
                    $('#subdistrict-dropdown').empty();
                    $('#subdistrict-dropdown').append('<option value="">-- กรุณาเลือกตำบล --</option>');
                    $.each(subdistricts, function (index, subdistrict) {
                        $('#subdistrict-dropdown').append('<option value="' + subdistrict.value + '">' + subdistrict.text + '</option>');
                    });
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching subdistricts: ' + error);
                }
            });
        } else {
            $('#subdistrict-dropdown').empty().append('<option value="">-- กรุณาเลือกตำบล --</option>');
        }
    });
    $("#save").click(function (event) {
        event.preventDefault(); // ป้องกัน Form จากการ Submit อัตโนมัติ

        var title = $("#title").val();
        var fname = $("#fname").val();
        var lname = $("#lname").val();
        var phone = $("#phone").val();
        var address = $("#address").val();
        //ภาพไม่มา
        var image = $('#image')[0].files; //เป็นการดึงข้อมูลรูปภาพเพื่อเตรียมเช็คไฟล์ก่อนทำงานส่วน Ajax                
        var provinceCode = $("#province-dropdown").val();
        var districtCode = $("#district-dropdown").val();
        var subdistrictCode = $("#subdistrict-dropdown").val();

        if (!fname || !lname || !phone || !address || !provinceCode) {
            //alert value
            alert("กรุณากรอกข้อมูลให้ครบถ้วน");
            return;
        }

        var formData = new FormData();
        formData.append("customerTitleName", title);
        formData.append("customerFName", fname);
        formData.append("customerLName", lname);
        formData.append("customerPhone", phone);
        formData.append("customerAddress", address);
        formData.append("provinceCode", provinceCode);
        formData.append("districtCode", districtCode);
        formData.append("subdistrictCode", subdistrictCode);
        formData.append("file", image[0]); //เพิ่มรูปภาพเข้าไปใน FormData

        $.ajax({
            url: '@Url.Action("Create", "Home")', // ใส่ URL ของ Controller ที่จะบันทึกข้อมูล
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                //alert("บันทึกข้อมูลสำเร็จ");
                //alert id alert show

                location.reload(); // รีโหลดหน้า
            },
            error: function (xhr, status, error) {
                console.error("Error: " + error);
                alert("เกิดข้อผิดพลาดในการบันทึกข้อมูล");
            }
        });
    });
});
