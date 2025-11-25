// =========================================================
// AngularJS App: Quản lý Bảo hành (Frontend kết nối ASP.NET)
// =========================================================
var app = angular.module("AppQuanLyBaoTri", []);

app.controller("WarrantyCtrl", function ($scope, $http) {
  const API_URL = "https://localhost:44310/api/BaoHanh";
  const API_TAISAN = "https://localhost:44310/api/TaiSan";

  $scope.warranties = [];
  $scope.assets = [];
  $scope.showModal = false;
  $scope.modalTitle = "Thêm bảo hành";
  $scope.warranty = {};

  // =========================================================
  // CHUYỂN JSON DATE → object Date
  // =========================================================
  function toDate(dateStr) {
    if (!dateStr) return null;
    if (dateStr instanceof Date) return dateStr;
    if (/^\d{4}-\d{2}-\d{2}$/.test(dateStr)) return new Date(dateStr);
    if (typeof dateStr === "string" && dateStr.includes("T"))
      return new Date(dateStr);
    return null;
  }

  // =========================================================
  // LOAD DANH SÁCH
  // =========================================================
  $scope.loadWarranties = function () {
    $http.get(API_URL + "/get-all").then(
      (res) => {
        res.data.forEach((x) => {
          x.ngayBatDau = toDate(x.ngayBatDau);
          x.ngayKetThuc = toDate(x.ngayKetThuc);
          x.maBaoHanh = parseInt(x.maBaoHanh);
        });
        $scope.warranties = res.data;
      },
      (err) => {
        console.error("❌ Lỗi load bảo hành:", err);
        alert("Không thể tải danh sách bảo hành!");
      }
    );
  };

  // =========================================================
  // LOAD TÀI SẢN
  // =========================================================
  $scope.loadAssets = function () {
    $http.get(API_TAISAN + "/get-all").then(
      (res) => {
        $scope.assets = res.data;
      },
      (err) => {
        console.error("❌ Lỗi load tài sản:", err);
        alert("Không thể tải danh sách tài sản!");
      }
    );
  };

  // =========================================================
  // MỞ / ĐÓNG MODAL
  // =========================================================
  $scope.openModal = function () {
    $scope.modalTitle = "Thêm bảo hành";
    $scope.warranty = {};
    $scope.showModal = true;
    $scope.loadAssets();
  };

  $scope.closeModal = function () {
    $scope.showModal = false;
    // Mở lại input và nút Lưu
    setTimeout(() => {
      const inputs = document.querySelectorAll(
        "#warrantyModal input, #warrantyModal select, #warrantyModal textarea"
      );
      inputs.forEach((i) => i.removeAttribute("disabled"));
      const saveBtn = document.querySelector(
        "#warrantyModal button[type='submit']"
      );
      if (saveBtn) saveBtn.style.display = "block";
    }, 50);
  };

  // =========================================================
  // FORMAT NGÀY (yyyy-MM-dd)
  // =========================================================
  function formatDate(d) {
    if (!d) return null;
    const m = d.getMonth() + 1;
    const day = d.getDate();
    return `${d.getFullYear()}-${m.toString().padStart(2, "0")}-${day
      .toString()
      .padStart(2, "0")}`;
  }

  // =========================================================
  // LƯU (THÊM / SỬA)
  // =========================================================
  $scope.saveWarranty = function () {
    let data = angular.copy($scope.warranty);

    data.maTaiSan = parseInt(data.maTaiSan);
    if (data.maBaoHanh) data.maBaoHanh = parseInt(data.maBaoHanh);
    data.ngayBatDau = formatDate(data.ngayBatDau);
    data.ngayKetThuc = formatDate(data.ngayKetThuc);

    // ======= UPDATE =======
    if (data.maBaoHanh) {
      $http.put(`${API_URL}/${data.maBaoHanh}`, data).then(
        (res) => {
          alert(res.data.message || "📝 Cập nhật thành công!");
          $scope.loadWarranties();
          $scope.closeModal();
        },
        (err) => {
          console.error("❌ Lỗi cập nhật:", err);
          alert("❌ Lỗi cập nhật bảo hành!");
        }
      );
      return;
    }

    // ======= CREATE =======
    $http.post(API_URL, data).then(
      (res) => {
        alert(res.data.message || "✔ Thêm bảo hành thành công!");
        $scope.loadWarranties();
        $scope.closeModal();
      },
      (err) => {
        console.error("❌ Lỗi thêm mới:", err);
        alert("❌ Không thể thêm bảo hành!");
      }
    );
  };

  // =========================================================
  // SỬA
  // =========================================================
  $scope.editWarranty = function (w) {
    const data = angular.copy(w);
    data.maBaoHanh = parseInt(data.maBaoHanh);
    data.maTaiSan = parseInt(data.maTaiSan);
    data.ngayBatDau = toDate(data.ngayBatDau);
    data.ngayKetThuc = toDate(data.ngayKetThuc);

    $scope.modalTitle = "Cập nhật bảo hành";
    $scope.warranty = data;
    $scope.showModal = true;
    $scope.loadAssets();
  };

  // =========================================================
  // XEM
  // =========================================================
  $scope.viewWarranty = function (w) {
    const data = angular.copy(w);
    data.ngayBatDau = toDate(data.ngayBatDau);
    data.ngayKetThuc = toDate(data.ngayKetThuc);

    $scope.modalTitle = "Chi tiết bảo hành";
    $scope.warranty = data;
    $scope.showModal = true;
    $scope.loadAssets();

    // 🧩 Khóa input + ẩn nút Lưu
    setTimeout(() => {
      const inputs = document.querySelectorAll(
        "#warrantyModal input, #warrantyModal select, #warrantyModal textarea"
      );
      inputs.forEach((i) => i.setAttribute("disabled", true));
      const saveBtn = document.querySelector(
        "#warrantyModal button[type='submit']"
      );
      if (saveBtn) saveBtn.style.display = "none";
    }, 50);
  };

  // =========================================================
  // XÓA
  // =========================================================
  $scope.deleteWarranty = function (id) {
    if (!confirm("🗑️ Bạn có chắc chắn muốn xóa bảo hành này không?")) return;

    $http.delete(`${API_URL}/${id}`).then(
      (res) => {
        alert(res.data.message || "Đã xóa thành công!");
        $scope.loadWarranties();
      },
      (err) => {
        console.error("❌ Lỗi xóa:", err);
        alert("❌ Không thể xóa bảo hành!");
      }
    );
  };

  // =========================================================
  // TÍNH TRẠNG BẢO HÀNH
  // =========================================================
  $scope.getWarrantyStatus = function (endDate) {
    const today = new Date();
    const end = new Date(endDate);
    const diffDays = (end - today) / (1000 * 60 * 60 * 24);

    if (diffDays < 0) return "Hết hạn";
    if (diffDays <= 30) return "Sắp hết hạn";
    return "Còn hạn";
  };

  $scope.getStatusColor = function (endDate) {
    const status = $scope.getWarrantyStatus(endDate);
    if (status === "Hết hạn") return "text-red-600";
    if (status === "Sắp hết hạn") return "text-yellow-600";
    return "text-green-600";
  };

  // =========================================================
  // KHỞI TẠO
  // =========================================================
  $scope.loadWarranties();
  $scope.loadAssets();
});
