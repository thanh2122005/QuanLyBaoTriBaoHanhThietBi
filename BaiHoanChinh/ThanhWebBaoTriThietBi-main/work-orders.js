// =========================================================
// AngularJS App: Quản lý Phiếu Công Việc (Frontend kết nối Backend ASP.NET)
// =========================================================
var app = angular.module("AppQuanLyBaoTri", []);

app.controller("PhieuCongViecCtrl", function ($scope, $http) {
  const API_PCV = "https://localhost:44310/api/PhieuCongViec";
  const API_NV = "https://localhost:44310/api/NhanVien";
  const API_TS = "https://localhost:44310/api/TaiSan";

  // ======= DỮ LIỆU KHỞI TẠO =======
  $scope.listPhieuCV = [];
  $scope.listNhanVien = [];
  $scope.listTaiSan = [];
  $scope.phieuCV = {};
  $scope.showModal = false;
  $scope.modalTitle = "Thêm phiếu công việc";

  // =========================================================
  // LOAD DỮ LIỆU
  // =========================================================
  $scope.loadData = function () {
    // --- Phiếu công việc ---
    $http.get(API_PCV + "/get-all").then(
      (res) => {
        // Nếu API trả về { message, data } thì lấy res.data.data
        // Nếu API trả về mảng trực tiếp thì dùng res.data
        $scope.listPhieuCV = res.data.data || res.data || [];
        console.log("📋 Phiếu công việc:", $scope.listPhieuCV);
      },
      (err) => {
        console.error("❌ Lỗi khi tải phiếu công việc:", err);
        $scope.listPhieuCV = [];
      }
    );

    // --- Nhân viên ---
    $http.get(API_NV + "/get-all").then(
      (res) => {
        $scope.listNhanVien = res.data.data || res.data || [];
        console.log("👨‍💼 Nhân viên:", $scope.listNhanVien);
      },
      (err) => {
        console.error("❌ Lỗi khi tải nhân viên:", err);
        $scope.listNhanVien = [];
      }
    );

    // --- Tài sản ---
    $http.get(API_TS + "/get-all").then(
      (res) => {
        $scope.listTaiSan = res.data.data || res.data || [];
        console.log("🏢 Tài sản:", $scope.listTaiSan);
      },
      (err) => {
        console.error("❌ Lỗi khi tải tài sản:", err);
        $scope.listTaiSan = [];
      }
    );
  };

  // =========================================================
  // MỞ / ĐÓNG MODAL
  // =========================================================
  $scope.openModal = function () {
    $scope.modalTitle = "Thêm phiếu công việc";
    $scope.phieuCV = {
      mucUuTien: "Trung bình",
      trangThai: "Mới",
      loai: "PM",
    };
    $scope.showModal = true;
  };

  $scope.closeModal = function () {
    $scope.showModal = false;
  };

  // =========================================================
  // THÊM / SỬA
  // =========================================================
  $scope.save = function () {
    if (!$scope.phieuCV.tieuDe || !$scope.phieuCV.maTaiSan) {
      alert("⚠️ Vui lòng nhập đầy đủ tiêu đề và chọn tài sản!");
      return;
    }

    // Đảm bảo có giá trị mặc định
    $scope.phieuCV.mucUuTien = $scope.phieuCV.mucUuTien || "Trung bình";
    $scope.phieuCV.trangThai = $scope.phieuCV.trangThai || "Mới";
    $scope.phieuCV.loai = $scope.phieuCV.loai || "PM";

    if ($scope.phieuCV.maPhieuCV) {
      // --- CẬP NHẬT ---
      $http
        .put(API_PCV + "/update/" + $scope.phieuCV.maPhieuCV, $scope.phieuCV)
        .then(
          (res) => {
            alert("✅ Cập nhật thành công!");
            $scope.loadData();
            $scope.closeModal();
          },
          (err) => {
            alert("❌ Lỗi khi cập nhật phiếu công việc!");
            console.error(err);
          }
        );
    } else {
      // --- THÊM MỚI ---
      $http.post(API_PCV + "/create", $scope.phieuCV).then(
        (res) => {
          alert("✅ Thêm mới thành công!");
          $scope.loadData();
          $scope.closeModal();
        },
        (err) => {
          alert("❌ Thêm thất bại! Vui lòng kiểm tra dữ liệu.");
          console.error(err);
        }
      );
    }
  };

  // =========================================================
  // SỬA
  // =========================================================
  $scope.edit = function (cv) {
    $scope.modalTitle = "Chỉnh sửa phiếu công việc";
    $scope.phieuCV = angular.copy(cv);
    $scope.showModal = true;
  };

  // =========================================================
  // XÓA
  // =========================================================
  $scope.xoa = function (id) {
    if (confirm("Bạn có chắc muốn xóa phiếu công việc này không?")) {
      $http.delete(API_PCV + "/delete/" + id).then(
        (res) => {
          alert("🗑️ Đã xóa thành công!");
          $scope.loadData();
        },
        (err) => {
          alert("❌ Lỗi khi xóa phiếu công việc!");
          console.error(err);
        }
      );
    }
  };

  // =========================================================
  // KHỞI CHẠY
  // =========================================================
  $scope.loadData();
});
