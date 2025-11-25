// =========================================================
// AngularJS App: Quản lý Tài khoản & Phân quyền (API chuẩn Backend của bạn)
// =========================================================
var app = angular.module("AppQuanLyBaoTri", []);

app.controller("TaiKhoanCtrl", function ($scope, $http) {

  const API = "https://localhost:44310/api/TaiKhoan";

  $scope.listTaiKhoan = [];
  $scope.tk = {};
  $scope.showModal = false;
  $scope.modalTitle = "";

  // =========================================================
  // 1) LOAD TÀI KHOẢN
  // =========================================================
  $scope.loadTaiKhoan = function () {
    $http({
      method: "GET",
      url: API + "/get-all"
    }).then(
      res => {
        $scope.listTaiKhoan = res.data;
        console.log("Danh sách tài khoản:", $scope.listTaiKhoan);
      },
      err => {
        console.error("Lỗi load tài khoản:", err);
        alert("Không thể tải dữ liệu tài khoản!");
      }
    );
  };

  // =========================================================
  // 2) MỞ MODAL THÊM
  // =========================================================
  $scope.openModal = function () {
    $scope.tk = {
      role: "NhanVien",
      trangThai: "Hoạt động"
    };
    $scope.modalTitle = "Thêm tài khoản";
    $scope.showModal = true;
  };

  // =========================================================
  // 3) MỞ MODAL SỬA
  // =========================================================
  $scope.editTaiKhoan = function (tk) {
    $scope.tk = angular.copy(tk);
    $scope.modalTitle = "Sửa tài khoản";
    $scope.showModal = true;
  };

  // =========================================================
  // 4) ĐÓNG MODAL
  // =========================================================
  $scope.closeModal = function () {
    $scope.showModal = false;
  };

  // =========================================================
  // 5) LƯU TÀI KHOẢN
  // =========================================================
  $scope.saveTaiKhoan = function () {

    if (!$scope.tk.tenDangNhap || !$scope.tk.matKhauHash) {
      alert("Vui lòng nhập đầy đủ thông tin!");
      return;
    }

    let method = "";
    let url = "";

    if ($scope.tk.maTaiKhoan) {
      // ---------------- UPDATE ----------------
      method = "PUT";
      url = API + "/update/" + $scope.tk.maTaiKhoan;
    } else {
      // ---------------- CREATE ----------------
      method = "POST";
      url = API + "/create";
    }

    $http({
      method: method,
      url: url,
      data: $scope.tk
    }).then(
      res => {
        alert(res.data.message || "Thành công!");
        $scope.closeModal();
        $scope.loadTaiKhoan();
      },
      err => {
        console.error("Lỗi lưu tài khoản:", err);
        alert("Không thể lưu tài khoản!");
      }
    );
  };

  // =========================================================
  // 6) XÓA TÀI KHOẢN
  // =========================================================
  $scope.deleteTaiKhoan = function (id) {

    if (!confirm("Bạn có chắc chắn muốn xóa tài khoản này không?")) return;

    $http({
      method: "DELETE",
      url: API + "/delete/" + id
    }).then(
      res => {
        alert(res.data.message || "Xóa thành công!");
        $scope.loadTaiKhoan();
      },
      err => {
        console.error("Lỗi xóa tài khoản:", err);
        alert("Không thể xóa tài khoản!");
      }
    );
  };

  // =========================================================
  // 7) KHỞI TẠO
  // =========================================================
  $scope.loadTaiKhoan();
});
