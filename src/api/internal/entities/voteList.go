package entities

type VoteList struct {
	Id     string              `bson:"Id"`
	UserId string              `bson:"UserId"`
	Title  string              `bson:"title"`
	Books  map[string][]string `bson:"Books"`
}
