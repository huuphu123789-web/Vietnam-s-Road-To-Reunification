code CONTRIBUTING.md
--------Quy tắc đóng góp---------
1. Quy trình đóng góp
Mỗi thành viên làm việc trên branch riêng thay vì trực tiếp trên main.

Khi hoàn thành, mở Pull Request (PR) để merge vào main.

PR cần được review bởi ít nhất một thành viên khác trước khi merge.

##################################################
2. Quy tắc đặt tên branch
feature/... cho tính năng mới

fix/... cho sửa lỗi

docs/... cho tài liệu

Ví dụ: feature-login, fix-bug-level1, docs-readme
##################################################
3. Quy tắc commit message
Commit message phải ngắn gọn, rõ ràng, mô tả đúng thay đổi.

Cấu trúc: <loại>: <mô tả>

Ví dụ:

feat: thêm màn hình đăng nhập

fix: sửa lỗi hiển thị nhân vật

docs: cập nhật README

##################################################
4. Kiểm thử và CI/CD
Trước khi mở PR, chạy test để đảm bảo không lỗi.

GitHub Actions sẽ tự động kiểm tra khi có PR.

Nếu có lỗi, sửa trước khi merge.
##################################################
5. Quản lý công việc
Dùng Issues để mô tả task.

Dùng Projects (Kanban board) để theo dõi tiến độ.

Gán task cho đúng người phụ trách.

Cập nhật trạng thái công việc thường xuyên.
##################################################
6. Góp ý và thảo luận
Nếu có ý tưởng mới, mở Issue để thảo luận.

Tôn trọng ý kiến của nhau, cùng hướng tới chất lượng dự án.

Tránh push trực tiếp vào main nếu chưa review.
##################################################
##################################################
##################################################
##################################################
--------Hướng dẫn sử dụng GitHub cho nhóm-------
1. Clone dự án về máy
Mỗi thành viên cần clone repo về máy:

bash
git clone https://github.com/huuphu123789-web/Vietnam-s-Road-To-Reunification.git
Sau đó di chuyển vào thư mục dự án:

bash
cd Vietnam-s-Road-To-Reunification
##################################################
2. Tạo branch riêng để làm việc
Không làm trực tiếp trên main.

Tạo branch mới cho từng tính năng hoặc task:

bash
git checkout -b feature-tenchucnang
##################################################
3. Commit và push thay đổi
Sau khi chỉnh sửa code hoặc tài liệu:

bash
git add .
git commit -m "feat: thêm màn hình đăng nhập"
git push origin feature-tenchucnang
##################################################
4. Mở Pull Request (PR)
Vào GitHub → mở PR từ branch của bạn vào main.

Mô tả rõ ràng thay đổi trong PR.

Chờ review từ ít nhất một thành viên khác trước khi merge.
##################################################
5. Đồng bộ với main
Trước khi push, luôn cập nhật branch của bạn:

bash
git pull origin main --rebase
Nếu có xung đột, tự xử lý rồi commit lại.
##################################################
6. Quản lý công việc trên GitHub
Dùng Issues để mô tả bug hoặc task.

Dùng Projects (Kanban board) để theo dõi tiến độ.

Gán task cho đúng người phụ trách.

Cập nhật trạng thái thường xuyên.
##################################################
7. Quy tắc chung
Không push trực tiếp vào main.

Luôn viết commit message rõ ràng.

Tôn trọng review của đồng đội.