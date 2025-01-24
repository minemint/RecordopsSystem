$(document).ready(function () {
    $('#province-dropdown').change(function () {
        var provinceCode = $(this).val();
        if (provinceCode) {
            $.ajax({
                type: "GET",
                url: "/Home/GetDistricts",
                data: { provinceCode: provinceCode },
                success: function (res) {
                    if (res) {
                        $("#district-dropdown").empty();
                        $("#district-dropdown").append('<option>-- กรุณาเลือกเขต --</option>');
                        $.each(res, function (key, value) {
                            $("#district-dropdown").append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                }
            });
        }
    });
    $('#district-dropdown').change(function () {
        var districtCode = $(this).val();
        if (districtCode) {
            $.ajax({
                type: "GET",
                url: "/Home/GetSubdistricts",
                data: { districtCode: districtCode },
                success: function (res) {
                    if (res) {
                        $("#subdistrict-dropdown").empty();
                        $("#subdistrict-dropdown").append('<option>-- กรุณาเลือกเขต --</option>');
                        $.each(res, function (key, value) {
                            $("#subdistrict-dropdown").append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                }
            });
        }
    });
}