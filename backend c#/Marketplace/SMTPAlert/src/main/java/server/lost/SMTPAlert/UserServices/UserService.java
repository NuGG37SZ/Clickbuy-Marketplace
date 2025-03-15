package server.lost.SMTPAlert.UserServices;

import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.ResponseEntity;
import java.util.Map;

@Service
public class UserService {
    private final RestTemplate restTemplate = new RestTemplate();
    private final String CSHARP_API_URL = "https://localhost:5098/api/v1/users/getByLogin/";

    public String getEmailByLogin(String login) {
        String url = CSHARP_API_URL + login;
        ResponseEntity<Map> response = restTemplate.getForEntity(url, Map.class);

        if (response.getBody() != null && response.getBody().containsKey("email")) {
            return (String) response.getBody().get("email");
        }

        return null;
    }
}
