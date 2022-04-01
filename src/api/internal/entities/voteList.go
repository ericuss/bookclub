package entities

type VoteList struct {
	Id            string              `bson:"Id"`
	UserId        string              `bson:"UserId"`
	Title         string              `bson:"title"`
	NumberOfVotes int16               `bson:"numberOfVotes"`
	Books         map[string][]string `bson:"Books"`
}
