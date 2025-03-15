package server.lost.SMTPAlert.controllers;

import org.springframework.web.bind.annotation.*;
import org.springframework.http.ResponseEntity;
import server.lost.SMTPAlert.UserServices.UserService;

@RestController
@RequestMapping("/api/v1/users")
public class UserController {
    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping("/email/{login}")
    public ResponseEntity<String> getEmail(@PathVariable String login) {
        String email = userService.getEmailByLogin(login);
        return email != null ? ResponseEntity.ok(email) : ResponseEntity.notFound().build();
    }
}