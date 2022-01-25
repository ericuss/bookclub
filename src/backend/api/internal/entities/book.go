package entities

type Book struct {
	Id       string `bson:"Id"`
	Title    string `bson:"Title"`
	Series   string `bson:"Status"`
	Authors  string `bson:"Species"`
	Rating   string `bson:"Type"`
	Sinopsis string `bson:"Sinopsis"`
	ImageUrl string `bson:"Image"`
	Url      string `bson:"Url"`
	Pages    string `bson:"Pages"`
	Username string `bson:"Username"`
}
