package com.example.demo;

import java.util.Locale;

import org.apache.log4j.Logger;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.MessageSource;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.support.ReloadableResourceBundleMessageSource;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import org.springframework.web.servlet.LocaleResolver;
import org.springframework.web.servlet.i18n.AcceptHeaderLocaleResolver;

@SpringBootApplication
@ComponentScan(basePackages="com.example.*")
@EntityScan(basePackages="com.example.*")
@EnableJpaRepositories(basePackages="com.example.*")
public class VehicleConfigIntegrationLoginRegWelcomeApplication implements CommandLineRunner {

    private static final Logger logger = Logger.getLogger(VehicleConfigIntegrationLoginRegWelcomeApplication.class);

    public static void main(String[] args) {
        SpringApplication.run(VehicleConfigIntegrationLoginRegWelcomeApplication.class, args);
    }

    @Override
    public void run(String... args) {
        logger.info("This is an INFO message");
        logger.debug("This is a DEBUG message");
        logger.error("This is an ERROR message");
    }

    @Bean
    public LocaleResolver localeResolver() {
        AcceptHeaderLocaleResolver localeResolver = new AcceptHeaderLocaleResolver();
        localeResolver.setDefaultLocale(Locale.ENGLISH); // Default language
        return localeResolver;
    }

    // Configure the MessageSource for i18n
    @Bean
    public MessageSource messageSource() {
        ReloadableResourceBundleMessageSource messageSource = new ReloadableResourceBundleMessageSource();
        messageSource.setBasename("classpath:messages");  // Base name of the messages files
        messageSource.setDefaultEncoding("UTF-8");
        return messageSource; // This line
    }
}     
        