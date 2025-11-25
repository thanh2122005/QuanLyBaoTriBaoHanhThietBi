// =========================================================
// AngularJS App: Chi tiết Phiếu kho (Frontend kết nối Backend ASP.NET Core)
// =========================================================
var app = angular.module("AppQuanLyBaoTri", []);

app.controller("ChiTietPhieuCtrl", function ($scope, $http) {
  // =========================================================
  // ⚙️ CẤU HÌNH API
  // =========================================================
  const API_CT = "https://localhost:44310/api/PhieuKho_ChiTiet";
  const API_LK = "https://localhost:44310/api/LinhKien";

  // =========================================================
  // 🧩 BIẾN LƯU TRỮ DỮ LIỆU
  // =========================================================
  $scope.listChiTiet = [];
  $scope.linhKienList = [];
  $scope.ct = {};
  $scope.showModal = false;
  $scope.isEdit = false;

  // =========================================================
  // 1️⃣ LOAD DANH SÁCH CHI TIẾT PHIẾU KHO
  // =========================================================
  $scope.LoadChiTietPhieuKho = function () {
    $http.get(API_CT + "/get-all").then(
      function (res) {
        $scope.listChiTiet = res.data.data || res.data || [];
      },
      function (err) {
        console.error("❌ Lỗi tải danh sách chi tiết:", err);
        alert("Không thể tải danh sách chi tiết phiếu kho!");
      }
    );
  };

  // =========================================================
  // 2️⃣ LOAD DANH SÁCH LINH KIỆN
  // =========================================================
  $scope.LoadLinhKien = function () {
    $http.get(API_LK + "/get-all").then(
      function (res) {
        $scope.linhKienList = res.data.data || res.data || [];
      },
      function (err) {
        console.error("❌ Lỗi tải linh kiện:", err);
        alert("Không thể tải danh sách linh kiện!");
      }
    );
  };

  // =========================================================
  // 3️⃣ LẤY TÊN LINH KIỆN THEO MÃ
  // =========================================================
  $scope.GetTenLinhKien = function (maLinhKien) {
    const item = $scope.linhKienList.find(
      (x) => x.maLinhKien === maLinhKien
    );
    return item ? item.tenLinhKien : "(Không tìm thấy)";
  };

  // =========================================================
  // 4️⃣ MỞ MODAL (Thêm hoặc Sửa)
  // =========================================================
  $scope.OpenModal = function () {
    $scope.ct = {}; // reset dữ liệu
    $scope.isEdit = false;
    $scope.showModal = true;
  };

  // Gọi khi bấm nút Sửa
  $scope.EditChiTietPhieuKho = function (ct) {
    $scope.ct = angular.copy(ct);
    $scope.isEdit = true;
    $scope.showModal = true;
  };

  $scope.CloseModal = function () {
    $scope.showModal = false;
  };

  // =========================================================
  // 5️⃣ LƯU (THÊM HOẶC SỬA)
  // =========================================================
  $scope.SaveChiTietPhieuKho = function () {
    if (!$scope.ct.maPhieuKho || !$scope.ct.maLinhKien || !$scope.ct.soLuong) {
      alert("⚠️ Vui lòng nhập đủ thông tin!");
      return;
    }

    if ($scope.isEdit && $scope.ct.maCT) {
      // === CẬP NHẬT ===
      $http.put(API_CT + "/update/" + $scope.ct.maCT, $scope.ct).then(
        function (res) {
          alert("✅ Cập nhật thành công!");
          $scope.CloseModal();
          $scope.LoadChiTietPhieuKho();
        },
        function (err) {
          console.error("❌ Lỗi cập nhật:", err);
          alert("Không thể cập nhật chi tiết phiếu kho!");
        }
      );
    } else {
      // === THÊM MỚI ===
      $http.post(API_CT + "/create", $scope.ct).then(
        function (res) {
          alert("✅ Thêm mới thành công!");
          $scope.CloseModal();
          $scope.LoadChiTietPhieuKho();
        },
        function (err) {
          console.error("❌ Lỗi thêm mới:", err);
          alert("Không thể thêm chi tiết phiếu kho!");
        }
      );
    }
  };

  // =========================================================
  // 6️⃣ XÓA
  // =========================================================
  $scope.DeleteChiTietPhieuKho = function (id) {
    if (!confirm("🗑️ Bạn có chắc chắn muốn xóa không?")) return;

    $http.delete(API_CT + "/delete/" + id).then(
      function (res) {
        alert("🗑️ Xóa thành công!");
        $scope.LoadChiTietPhieuKho();
      },
      function (err) {
        console.error("❌ Lỗi xóa:", err);
        alert("Không thể xóa chi tiết phiếu kho!");
      }
    );
  };

  // =========================================================
  // 7️⃣ KHỞI TẠO
  // =========================================================
  $scope.LoadChiTietPhieuKho();
  $scope.LoadLinhKien();
});
