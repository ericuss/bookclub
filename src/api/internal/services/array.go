package services

func StringInSlice(a string, list []string) bool {
	if list == nil {
		return false
	}
	for _, b := range list {
		if b == a {
			return true
		}
	}
	return false
}

func RemoveValue(s []string, str string) []string {
	newLength := 0
	for index := range s {
		if s[index] != str {
			s[newLength] = s[index]
			newLength++
		}
	}

	return s[:newLength]
}
