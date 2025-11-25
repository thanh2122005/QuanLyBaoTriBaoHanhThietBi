// =============================================================
// auth.js - Quản lý đăng nhập & phân quyền cho toàn hệ thống
// =============================================================
document.addEventListener("DOMContentLoaded", () => {
  // Ẩn sidebar tạm thời để tránh nháy
  document.body.classList.add("loading");

  const user = JSON.parse(localStorage.getItem("userLogin"));
  const role = localStorage.getItem("userRole");

  // ===== 1️⃣ Nếu chưa đăng nhập → quay lại login =====
  if (!user) {
    alert("⚠️ Vui lòng đăng nhập trước!");
    window.location.href = "login.html";
    return;
  }

  // ===== 2️⃣ Hiển thị thông tin người dùng =====
  const userInfo = document.getElementById("userInfo");
  if (userInfo) {
    userInfo.textContent = `Xin chào: ${user.fullName || user.tenDangNhap} (${role})`;
  }

  // ===== 3️⃣ Danh sách trang nhân viên được phép vào =====
  const allowedPages = [
    "dashboard.html",
    "assets.html",
    "customers.html",
    "warranties.html",
    "work-orders.html",
    "incidents.html",
    "inventory.html",
    "warehouse.html",
    "warehouse-detail.html"
  ];

  // ===== 4️⃣ Nếu nhân viên truy cập trang bị cấm → chặn lại =====
  const currentPage = window.location.pathname.split("/").pop();
  if (role === "NhanVien" && !allowedPages.includes(currentPage)) {
    alert("🚫 Bạn không có quyền truy cập trang này!");
    window.location.href = "dashboard.html";
    return;
  }

  // ===== 5️⃣ Nếu là nhân viên → ẩn menu sidebar không được phép =====
  if (role === "NhanVien") {
    const allowedMenus = allowedPages;

    document.querySelectorAll(".nav-link").forEach((link) => {
      const href = link.getAttribute("href");
      if (href && !allowedMenus.includes(href)) {
        link.style.display = "none";
      }
    });
  }

  // ===== 6️⃣ Đăng xuất =====
  const logoutBtn = document.getElementById("logoutBtn");
  if (logoutBtn) {
    logoutBtn.addEventListener("click", () => {
      if (confirm("Bạn có chắc muốn đăng xuất không?")) {
        localStorage.clear();
        window.location.href = "login.html";
      }
    });
  }

  // ===== 7️⃣ Hiển thị lại sidebar sau khi xử lý role =====
  document.body.classList.remove("loading");
});
