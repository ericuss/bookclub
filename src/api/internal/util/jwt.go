package util

import (
	"strings"
	"time"

	"github.com/dgrijalva/jwt-go"
)

const SecretKey = "secret"

func GenerateJwt(issuer string) (string, error) {
	claims := jwt.NewWithClaims(jwt.SigningMethodHS256, jwt.StandardClaims{
		Issuer:    issuer,
		ExpiresAt: time.Now().Add(time.Hour * 24).Unix(),
	})

	return claims.SignedString([]byte(SecretKey))
}

func ParseJwt(tokenString string) (string, error) {
	tokenString = strings.Replace(strings.Replace(tokenString, "Bearer ", "", -1), "bearer ", "", -1)

	token, err := jwt.ParseWithClaims(tokenString, &jwt.StandardClaims{}, func(t *jwt.Token) (interface{}, error) {
		return []byte(SecretKey), nil
	})

	if err != nil {
		return "", err
	}

	claims := token.Claims.(*jwt.StandardClaims)

	return claims.Issuer, err
}
