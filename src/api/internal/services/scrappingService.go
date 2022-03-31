package services

import (
	"log"

	entities "bookclubapi/internal/entities"

	"github.com/gocolly/colly"
)

type ScrappingService interface {
	Execute(endpoint string) (*entities.Book, error)
}

type scrappingService struct {
}

func NewScrappingService() *scrappingService {
	return &scrappingService{}
}

func (r *scrappingService) Execute(endpoint string) (*entities.Book, error) {
	// endpoing := "https://www.goodreads.com/book/show/2767793-the-hero-of-ages"

	c := colly.NewCollector(
	// colly.AllowedDomains("www.goodreads.com"),
	)
	book := &entities.Book{}
	c.OnHTML(".mainContent", func(e *colly.HTMLElement) {
		log.Println("Scraping...")

		book.Title = e.ChildText("#bookTitle")
		book.Series = e.ChildText("#bookSeries")
		book.Authors = e.ChildText("#bookAuthors .authorName ")
		book.Rating = e.ChildText("#bookMeta [itemprop=ratingValue]")
		book.Sinopsis = e.ChildText("#description")
		// book.ImageUrl = "https://www.goodreads.com" + e.ChildAttr(".bookCoverContainer [itemprop=image]", "href")
		book.ImageUrl = e.ChildAttr("#coverImage", "src")
		book.Url = endpoint
		book.Pages = e.ChildText("#details [itemprop=numberOfPages]")

	})
	c.Visit(endpoint)
	c.Wait()

	log.Println("Scraping complete")
	return book, nil
}
