package server.lost.SMTPAlert.UserServices;

import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.core.publisher.Mono;

@Service
public class UserClientService {

    private final WebClient webClient;

    public UserClientService() {
        this.webClient = WebClient.create("https://localhost:5098/api/v1/users");
    }

    public Mono<UserDTO> getUserByLogin(String login) {
        return webClient.get()
                .uri("/getByLogin/{login}", login) 
                .retrieve()
                .bodyToMono(UserDTO.class);
    }
}
