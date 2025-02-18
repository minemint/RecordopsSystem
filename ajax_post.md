```
$(document).ready(function () {
            $('#login').click(function () {
    var model = {
        username: $('#username').val(),
        password: $('#password').val()
    };
});

});


 [HttpPost]
 public async Task<IActionResult> Login([FromBody] LoginViewModel model)
 {
     if (model == null)
     {
         return BadRequest(new { success = false, message = "Invalid request data" });
     }

     try
     {
         if (model == null)
         {
             return BadRequest(new { success = false, message = "Invalid request data" });
         }
         var json = JsonConvert.SerializeObject(model);
         var data = new StringContent(json, Encoding.UTF8, "application/json");
         HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Auth/login", data);

         if (response.IsSuccessStatusCode)
         {
             var dataResponse = await response.Content.ReadAsStringAsync();
             var user = JsonConvert.DeserializeObject<UserViewModel>(dataResponse);

             if (user?.Data == null || string.IsNullOrEmpty(user.Data.Token))
             {
                 return BadRequest(new { success = false, message = "Invalid user data received from server." });
             }

             // บันทึก session
             HttpContext.Session.SetString("Token", user.Data.Token);
             HttpContext.Session.SetString("Username", user.Data.Username);
             HttpContext.Session.SetString("Role", user.Data.Role);

             // ส่ง URL redirect เป็น JSON response
             var redirectUrl = user.Data.Role == "Admin"
                 ? Url.Action("Index", "Dashboard", new { area = "Admin" })
                 : Url.Action("Index", "Dashboard", new { area = "User" });

             return Json(new { success = true, redirectUrl });
         }
         else
         {
             return Json(new { success = false, message = "Username or Password is incorrect." });
         }
     }
     catch (Exception ex)
     {
         return StatusCode(500, new { success = false, message = "Internal server error.", error = ex.Message });
     }
 }
```
