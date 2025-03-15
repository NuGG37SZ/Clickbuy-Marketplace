package server.lost.SMTPAlert.UserServices;

import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.ResponseEntity;
import server.lost.SMTPAlert.UserServices.UserDTO;

@Service
public class UserService {

    private final RestTemplate restTemplate = new RestTemplate();
    private final String CSHARP_API_URL = "https://localhost:5098/api/v1/users/getByLogin/";

    public String getEmailByLogin(String login) {
        String url = CSHARP_API_URL + login;

        try {
            ResponseEntity<UserDTO> response = restTemplate.getForEntity(url, UserDTO.class);

            if (response.getBody() != null) {
                UserDTO user = response.getBody();
                return user.getEmail();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        return null;
    }
}
