package server.lost.SMTPAlert.UserServices;

public class UserDTO {
    private String id;
    private String login;
    private String password;
    private String email;
    private String role;

    // Геттеры и сеттеры

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

}
