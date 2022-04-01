package commands

import (
	"bookclubapi/internal/entities"
	repositories "bookclubapi/internal/repositories"

	"github.com/google/uuid"
)

type AddVoteListRequest struct {
	UserId        string
	Title         string   `json:"title"`
	NumberOfVotes int16    `json:"numberOfVotes,omitempty,string"`
	Books         []string `json:"books"`
}

type addVoteListHandler struct {
	repository repositories.VoteListRepository
}

func NewAddVoteListHandler() *addVoteListHandler {
	return &addVoteListHandler{
		repository: repositories.NewVoteListRepository(),
	}
}

func (h *addVoteListHandler) Handler(request AddVoteListRequest) (*entities.VoteList, error) {
	id, _ := uuid.NewRandom()
	voteList := &entities.VoteList{
		Id:            id.String(),
		UserId:        request.UserId,
		NumberOfVotes: request.NumberOfVotes,
		Title:         request.Title,
	}

	voteList.Books = make(map[string][]string)
	for _, v := range request.Books {
		voteList.Books[v] = make([]string, 0)
	}

	addError := h.repository.Add(voteList)

	if addError != nil {
		return nil, addError
	}

	return voteList, nil
}
