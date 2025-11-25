// =========================================================
// AngularJS App: Đăng nhập hệ thống Bảo trì
// =========================================================
var app = angular.module("AppQuanLyBaoTri", []);

app.controller("LoginCtrl", function ($scope, $http, $timeout) {
  const API_URL = "https://localhost:44310/api/TaiKhoan/get-all";

  $scope.username = "";
  $scope.password = "";

  // =========================================================
  // HÀM ĐĂNG NHẬP
  // =========================================================
  $scope.login = function () {
    // Kiểm tra có nhập đủ thông tin chưa
    if (!$scope.username || !$scope.password) {
      alert("⚠️ Vui lòng nhập tên đăng nhập và mật khẩu!");
      return;
    }

    // Gọi API lấy danh sách tài khoản
    $http.get(API_URL).then(
      function (response) {
        const users = response.data || [];

        // Tìm user khớp với username và password
        const user = users.find(
          (u) =>
            u.tenDangNhap === $scope.username &&
            u.matKhauHash === $scope.password
        );

        // Nếu không tìm thấy user
        if (!user) {
          alert("❌ Sai tên đăng nhập hoặc mật khẩu!");
          return;
        }

        // Nếu tài khoản bị khóa
        if (user.trangThai === "Khóa") {
          alert("🔒 Tài khoản đã bị khóa!");
          return;
        }

        // Lưu thông tin đăng nhập vào localStorage
        localStorage.setItem("userLogin", JSON.stringify(user));
        localStorage.setItem("userRole", user.role);

        alert("✅ Đăng nhập thành công!");

        // Chuyển sang trang dashboard
        window.location.href = "./dashboard.html";
      },
      function (error) {
        console.error("❌ Lỗi khi đăng nhập:", error);
        alert("❌ Không thể kết nối tới server!");
      }
    );
  };

  // =========================================================
  // ⭐ HIỆN / ẨN MẬT KHẨU KHI CLICK VÀO ICON KHÓA ⭐
  // =========================================================
  
  // Đợi Angular render xong DOM rồi mới chạy
  $timeout(function () {
    const toggleButton = document.getElementById("togglePassword");
    const passwordInput = document.getElementById("passwordInput");
    const lockIcon = document.getElementById("lockIcon");

    // Kiểm tra xem các element có tồn tại không
    if (!toggleButton || !passwordInput || !lockIcon) {
      console.error("❌ Không tìm thấy element togglePassword, passwordInput hoặc lockIcon");
      return;
    }

    // Khi click vào icon khóa
    toggleButton.addEventListener("click", function () {
      
      // Kiểm tra xem đang ẩn hay hiện mật khẩu
      const isPasswordHidden = passwordInput.type === "password";

      if (isPasswordHidden) {
        // Nếu đang ẩn → Hiện mật khẩu
        passwordInput.type = "text";
        
        // Đổi icon sang dạng "mở khóa"
        lockIcon.innerHTML = `
          <path stroke-linecap="round" stroke-linejoin="round"
            d="M12 1v11m4-4H8m12 4v7a2 2 0 01-2 2H6a2 2 0 01-2-2v-7a2 2 0 012-2h12a2 2 0 012 2z"/>
        `;
      } else {
        // Nếu đang hiện → Ẩn mật khẩu
        passwordInput.type = "password";
        
        // Đổi icon sang dạng "khóa"
        lockIcon.innerHTML = `
          <path stroke-linecap="round" stroke-linejoin="round"
            d="M16 10V7a4 4 0 10-8 0v3m-2 0h12a2 2 0 012 2v7a2 2 0 01-2 2H6a2 2 0 01-2-2v-7a2 2 0 012-2z"/>
        `;
      }
    });
  }, 0);

});