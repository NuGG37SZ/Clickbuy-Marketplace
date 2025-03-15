package server.lost.SMTPAlert;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
public class SmtpAlertApplication {

	public static void main(String[] args) {
		SpringApplication.run(SmtpAlertApplication.class, args);
	}

}
