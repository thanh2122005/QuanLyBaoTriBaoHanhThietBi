var app = angular.module("ForgotApp", []);

app.controller("ForgotCtrl", function ($scope, $http) {

    // API ĐÚNG 100%
const API = "https://localhost:44310/api/TaiKhoan";


    $scope.step = 1;

    $scope.sendOtp = function () {
        if (!$scope.email) {
            alert("⚠ Vui lòng nhập email!");
            return;
        }

        $http.post(API + "/send-otp", { email: $scope.email })
            .then(function (res) {
                alert("📩 OTP đã được gửi đến email!");
                $scope.step = 2;
            })
            .catch(function (err) {
                console.error(err);
                alert("❌ Email không tồn tại hoặc lỗi gửi OTP!");
            });
    };

    $scope.verifyOtp = function () {
        $http.post(API + "/verify-otp", {
            email: $scope.email,
            otp: $scope.otp
        })
            .then(function (res) {
                alert("✔ OTP chính xác!");
                $scope.step = 3;
            })
            .catch(function (err) {
                console.error(err);
                alert("❌ Mã OTP sai!");
            });
    };

    $scope.resetPassword = function () {
        $http.post(API + "/reset-password", {
            email: $scope.email,
            newPassword: $scope.newPassword
        })
            .then(function (res) {
                alert("🎉 Đổi mật khẩu thành công!");
                window.location.href = "login.html";
            })
            .catch(function (err) {
                console.error(err);
                alert("❌ Không thể đặt lại mật khẩu!");
            });
    };

});
