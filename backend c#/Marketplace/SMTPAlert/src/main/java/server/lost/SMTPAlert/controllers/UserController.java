package server.lost.SMTPAlert.controllers;

import server.lost.SMTPAlert.UserServices.UserService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/v1/users")
public class UserController {

    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping("/email/{login}")
    public ResponseEntity<String> getEmailByLogin(@PathVariable String login) {
        System.out.println("Request received for login: " + login);

        String email = userService.getEmailByLogin(login);

        if (email != null) {
            return ResponseEntity.ok(email);
        } else {
            System.out.println("Email not found for login: " + login);
            return ResponseEntity.notFound().build();
        }
    }
}
