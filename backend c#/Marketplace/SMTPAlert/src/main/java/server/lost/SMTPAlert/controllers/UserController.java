package server.lost.SMTPAlert.controllers;

import org.springframework.web.bind.annotation.*;
import reactor.core.publisher.Mono;
import server.lost.SMTPAlert.UserServices.UserClientService;
import server.lost.SMTPAlert.UserServices.UserDTO;

@RestController
@RequestMapping("/users")
public class UserController {

    private final UserClientService userClientService;

    public UserController(UserClientService userClientService) {
        this.userClientService = userClientService;
    }

    @GetMapping("/getByLogin/{login}")
    public Mono<UserDTO> getUserByLogin(@PathVariable String login) {
        return userClientService.getUserByLogin(login);
    }
}
