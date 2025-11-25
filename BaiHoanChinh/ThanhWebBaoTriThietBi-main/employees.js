// =========================================================
// AngularJS App: Quản lý Nhân viên (Frontend ASP.NET)
// =========================================================
var app = angular.module("AppQuanLyBaoTri", []);

app.controller("NhanVienCtrl", function ($scope, $http) {

  // API Backend
  const API = "https://localhost:44310/api/NhanVien";

  // Dữ liệu
  $scope.listNhanVien = [];
  $scope.nhanvien = {};
  $scope.showModal = false;
  $scope.modalTitle = "";
  $scope.submitMode = "create"; // create | update

  // =========================================================
  // LOAD TẤT CẢ NHÂN VIÊN
  // =========================================================
  $scope.LoadNhanVien = function () {
    $http({
      method: "GET",
      url: API + "/get-all"
    }).then(function (res) {
      $scope.listNhanVien = res.data;
      console.log("Danh sách nhân viên:", $scope.listNhanVien);
    }, function (err) {
      console.error("Lỗi load:", err);
      alert("❌ Không thể tải danh sách nhân viên!");
    });
  };

  // =========================================================
  // MỞ MODAL THÊM NHÂN VIÊN
  // =========================================================
  $scope.openModal = function () {
    $scope.nhanvien = { trangThai: "Hoạt động" };
    $scope.submitMode = "create";
    $scope.modalTitle = "Thêm nhân viên";
    $scope.showModal = true;
  };

  // =========================================================
  // MỞ MODAL SỬA NHÂN VIÊN
  // =========================================================
  $scope.EditNhanVien = function (nv) {
    $scope.nhanvien = angular.copy(nv);
    $scope.submitMode = "update";
    $scope.modalTitle = "Cập nhật nhân viên";
    $scope.showModal = true;
  };

  // =========================================================
  // ĐÓNG MODAL
  // =========================================================
  $scope.closeModal = function () {
    $scope.showModal = false;
  };

  // =========================================================
  // LƯU NHÂN VIÊN (THÊM / SỬA)
  // =========================================================
  $scope.SaveNhanVien = function () {

    let data = {
      maNV: $scope.nhanvien.maNV,
      hoTen: ($scope.nhanvien.hoTen || "").trim(),
      email: $scope.nhanvien.email || "",
      soDienThoai: $scope.nhanvien.soDienThoai || "",
      trangThai: $scope.nhanvien.trangThai || "Hoạt động"
    };

    if (!data.hoTen) {
      alert("⚠️ Vui lòng nhập họ tên nhân viên!");
      return;
    }

    if ($scope.submitMode === "create") {
      // --------------------- THÊM ---------------------
      $http({
        method: "POST",
        url: API + "/create",
        data: data
      }).then(function (res) {
        alert(res.data.message || "Thêm nhân viên thành công!");
        $scope.closeModal();
        $scope.LoadNhanVien();
      }, function (err) {
        console.error("Lỗi thêm:", err);
        alert("❌ Lỗi khi thêm nhân viên!");
      });

    } else {
      // --------------------- CẬP NHẬT ---------------------
      $http({
        method: "PUT",
        url: API + "/update/" + data.maNV,
        data: data
      }).then(function (res) {
        alert(res.data.message || "Cập nhật nhân viên thành công!");
        $scope.closeModal();
        $scope.LoadNhanVien();
      }, function (err) {
        console.error("Lỗi cập nhật:", err);
        alert("❌ Lỗi khi cập nhật nhân viên!");
      });
    }
  };

  // =========================================================
  // XÓA NHÂN VIÊN (XÓA MỀM → chuyển 'Nghỉ việc')
  // =========================================================
  $scope.DeleteNhanVien = function (id) {
    if (!confirm("🗑️ Chuyển nhân viên này sang trạng thái 'Nghỉ việc'?")) return;

    $http({
      method: "DELETE",
      url: API + "/delete/" + id
    }).then(function (res) {
      alert(res.data.message || "Cập nhật trạng thái thành công!");
      $scope.LoadNhanVien();
    }, function (err) {
      console.error("Lỗi xóa:", err);
      alert("❌ Không thể cập nhật trạng thái nhân viên!");
    });
  };

  // =========================================================
  // KHỞI TẠO
  // =========================================================
  $scope.LoadNhanVien();
});
