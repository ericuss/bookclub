package main

import (
	"encoding/csv"
	"fmt"
	"log"
	"os"

	"github.com/gocolly/colly"
)

func main() {
	fmt.Println("running")

	fName := "data.csv"
	file, err := os.Create(fName)

	if err != nil {
		log.Fatalf("Could not create file, err :%q", err)
		return
	}

	defer file.Close()

	writer := csv.NewWriter(file)
	defer writer.Flush()
	endpoing := "https://www.goodreads.com/book/show/2767793-the-hero-of-ages"

	c := colly.NewCollector(
	// colly.AllowedDomains("www.goodreads.com"),
	)

	c.OnHTML(".mainContent", func(e *colly.HTMLElement) {
		log.Println("Scraping complete")

		log.Println(e.ChildAttr(".bookCoverContainer [itemprop=image]", "href"))
		log.Println(e.ChildText("#bookTitle"))
		log.Println(e.ChildText("#bookSeries"))
		log.Println(e.ChildText("#bookAuthors .authorName "))
		log.Println(e.ChildText("#bookMeta [itemprop=ratingValue]"))
		log.Println(e.ChildText("#description"))
		log.Println(e.ChildText("#details [itemprop=numberOfPages]"))
	})
	c.Visit(endpoing)

	log.Println("Scraping complete")
	log.Println(c)
}
