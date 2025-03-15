package server.lost.SMTPAlert.Config;

import javax.net.ssl.*;
import java.io.*;
import java.security.*;
import java.security.cert.*;
import java.security.cert.Certificate;
import java.util.*;

public class SSLConfig {
    public static void main(String[] args) {
        try {
            // Пути к вашим сертификатам
            String certFilePath1 = "D:/Work/certificate1.crt";
            String certFilePath2 = "D:/Work/certificate2.crt";

            // Создание KeyStore для загрузки сертификатов
            KeyStore keyStore = KeyStore.getInstance(KeyStore.getDefaultType());
            keyStore.load(null, null);  // Инициализируем пустой Keystore

            // Загрузим первый сертификат
            try (FileInputStream certFile1 = new FileInputStream(certFilePath1)) {
                CertificateFactory certificateFactory = CertificateFactory.getInstance("X.509");
                Certificate certificate1 = certificateFactory.generateCertificate(certFile1);
                keyStore.setCertificateEntry("cert1", certificate1);
            }

            // Загрузим второй сертификат
            try (FileInputStream certFile2 = new FileInputStream(certFilePath2)) {
                CertificateFactory certificateFactory = CertificateFactory.getInstance("X.509");
                Certificate certificate2 = certificateFactory.generateCertificate(certFile2);
                keyStore.setCertificateEntry("cert2", certificate2);
            }

            // Создаем TrustManager, который использует наш KeyStore
            TrustManagerFactory trustManagerFactory = TrustManagerFactory.getInstance(TrustManagerFactory.getDefaultAlgorithm());
            trustManagerFactory.init(keyStore);
            TrustManager[] trustManagers = trustManagerFactory.getTrustManagers();

            // Создаем SSLContext с нашим TrustManager
            SSLContext sslContext = SSLContext.getInstance("TLS");
            sslContext.init(null, trustManagers, new SecureRandom());

            // Устанавливаем SSLContext для REST-запросов
            HttpsURLConnection.setDefaultSSLSocketFactory(sslContext.getSocketFactory());

            // Теперь можно делать HTTPS запросы с обоими сертификатами
            System.out.println("SSLContext установлен с двумя сертификатами.");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
