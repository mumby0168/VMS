package main

import (
	"fmt"
	"os"
)

func getServices() []string {
	return []string{"User", "Visitor"}
}

func printError(message string) {
	fmt.Println("ERROR: " + message)
}

func checkParams(inputs []string) []string {
	services := getServices()
	var newServices []string
	for _, service := range inputs {
		if !contains(service, services) {
			printError(service + " is not a registered service")
			continue
		}
		newServices = append(newServices, service)
	}

	return newServices
}

func contains(check string, data []string) bool {
	for _, item := range data {
		if item == check {
			return true
		}
	}

	return false
}

func main() {
	argument := os.Args[1:]

	filteredServices := checkParams(argument)

	fmt.Println(filteredServices)
}
