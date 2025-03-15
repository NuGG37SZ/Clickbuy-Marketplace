package server.lost.SMTPAlert;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan("server.lost.SMTPAlert")
public class SmtpAlertApplication {

	public static void main(String[] args) {
		try {
			// Отключаем проверку SSL
			SSLUtils.disableSSLVerification();
		} catch (Exception e) {
			e.printStackTrace();
		}

		SpringApplication.run(SmtpAlertApplication.class, args);
	}
}
