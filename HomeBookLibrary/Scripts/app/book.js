var ViewModel = function() {
    var self = this;
    var booksUri = "/api/books/";
    var authorsUri = "/api/authors/";
    var genresUri = "/api/genres/";
    var loansUri = "/api/loans/";
    var currentDate = (new Date()).toISOString().split("T")[0];

    self.books = ko.observableArray();
    self.error = ko.observable();
    self.authors = ko.observableArray();
    self.titles = ko.observableArray();
    self.genres = ko.observableArray();
    self.selectedAuthor = ko.observable();
    self.selectedTitle = ko.observable();
    self.selectedGenre = ko.observable();
    self.selectedIsbn = ko.observable();
    self.bookDetail = ko.observable();
    self.filteredBooks = ko.observable();
    self.loanedBook = ko.observable();
    self.showing = ko.observable();
    self.message = ko.observable();
    self.loans = ko.observable();

    self.show = function(panel) {
        return (function() { self.showing(panel); });
    };
    self.newLoanBook = {
        Book: self.bookDetail,
        Title: ko.observable(),
        DateLoaned: ko.observable(moment(currentDate).format("DD-MM-YYYY")),
        Name: ko.observable(),
        Surname: ko.observable(),
        Comments: ko.observable()
    };

    function ajaxHelper(uri, method, data) {
        self.error(""); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: "json",
            contentType: "application/json",
            data: data ? JSON.stringify(data) : null
        }).fail(function(jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllBooks() {
        ajaxHelper(booksUri, "GET").done(function(data) {
            self.filteredBooks(data);
            self.showing("books");
        });
    }

    function getAllAuthors() {
        ajaxHelper(authorsUri, "GET").done(function(data) {
            self.authors(data);
        });
    }

    function getAllGenres() {
        ajaxHelper(genresUri, "GET").done(function(data) {
            self.genres(data);
        });
    }

    function getAllLoans() {
        ajaxHelper(loansUri, "GET").done(function(data) {
            console.log(data);
            self.loans(data);
        });
    }


    function formatUrl(selected) {
        var returnString = "";

        if (selected != null) {
            returnString += selected;
        } else {
            returnString += 0;
        }
        returnString += "/";

        return returnString;
    }

    self.doSearch = function() {
        var queryString = "";
        queryString += formatUrl(self.selectedAuthor());
        queryString += formatUrl(self.selectedTitle());
        queryString += formatUrl(self.selectedGenre());
        queryString += formatUrl(self.selectedIsbn());

        ajaxHelper(booksUri + queryString, "GET").done(function(data) {
            self.filteredBooks(data);
            self.showing("books");
            self.message("");
        });

    };

    self.getBookDetail = function(item) {
        ajaxHelper(booksUri + item.Id, "GET").done(function(data) {
            self.bookDetail(data);
            self.showing("bookDetails");
            self.message("");
        });
    };
    self.loanBook = function(item) {
        ajaxHelper(booksUri + item.Id, "GET").done(function(data) {
            self.loanedBook = data;
            self.bookDetail(data);
            self.showing("Loanees");
            self.message("");
        });
    };
    self.loanBookSubmit = function(formElement) {
        var loannee = {
            BookId: this.loanedBook.Id,
            DateLoaned: moment(self.newLoanBook.DateLoaned()).format("DD-MM-YYYY"),
            Name: self.newLoanBook.Name(),
            Surname: self.newLoanBook.Surname(),
            Comments: self.newLoanBook.Comments()
        };


        ajaxHelper(loansUri, "POST", loannee).done(function(item) {
            self.books.push(item);
            self.message("Loan Book Updated!");
            //self.filteredBooks();
            getAllBooks();
        });
    };
    self.returnBook = function(item) {
        ajaxHelper(loansUri + item.Id, "DELETE").done(function(data) {
            getAllLoans();
            self.message("Book Available again!");
        });
    };
    getAllBooks();
    getAllAuthors();
    getAllGenres();
    getAllLoans();
};

ko.applyBindings(new ViewModel());