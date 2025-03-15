package server.lost.SMTPAlert;

import java.io.IOException;

public class StartServer {
    public static void main(String[] args) {
        try {
            // Путь к JAR файлу вашего Spring Boot приложения
            String jarPath = "build/libs/server.jar";  // Убедитесь, что путь к JAR файлу правильный

            // Запуск команды для открытия нового окна консоли и выполнения команды java -jar
            ProcessBuilder processBuilder = new ProcessBuilder(
                    "cmd.exe", "/c", "start", "java -jar " + jarPath
            );

            processBuilder.inheritIO();  // Наследуем ввод/вывод от родительского процесса
            processBuilder.start();  // Запуск процесса

            System.out.println("Сервер запущен в новом окне консоли!");
        } catch (IOException e) {
            e.printStackTrace();  // Обработка ошибок
        }
    }
}
