package com.example.model;

import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.annotation.*;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Component;

@Aspect
@Component
public class LoggingAspect {

    private static final Logger logger = LoggerFactory.getLogger(LoggingAspect.class);

    
    @Pointcut("execution(* com.example.service.*.*(..))")
    public void serviceMethods() {}

   
    @Before("serviceMethods()")
    public void beforeMethodExecution(JoinPoint joinPoint) {
        logger.info("Executing method: " + joinPoint.getSignature().getName() + " in " + joinPoint.getTarget().getClass().getSimpleName());
        System.out.println("before Method");
    }

    
    @After("serviceMethods()")
    public void afterMethodExecution(JoinPoint joinPoint) {
        logger.info("Completed method: " + joinPoint.getSignature().getName());
    }

    
    @AfterThrowing(pointcut = "serviceMethods()", throwing = "ex")
    public void afterThrowingException(JoinPoint joinPoint, Exception ex) {
        logger.error("Exception in method: " + joinPoint.getSignature().getName() + " | Exception: " + ex.getMessage());
    }

    
    @Around("serviceMethods()")
    public Object logExecutionTime(ProceedingJoinPoint joinPoint) throws Throwable {
        long startTime = System.currentTimeMillis();
        Object result = joinPoint.proceed();  // Execute actual method
        long timeTaken = System.currentTimeMillis() - startTime;
        
        logger.info("Execution time of " + joinPoint.getSignature().getName() + " : " + timeTaken + "ms");
        return result;
    }
}

