var ViewModel = function () {
    var self = this;
    var booksUri = '/api/books/';
    var authorsUri = '/api/authors/';
    var genresUri = '/api/genres/';
    var currentDate = (new Date()).toISOString().split('T')[0];

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

    self.newLoanBook = {
        Title: ko.observable(),
        CurrentDate: ko.observable(currentDate),
        Name: ko.observable(),
        Comments: ko.observable()
    }

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllAuthors() {
        ajaxHelper(authorsUri, 'GET').done(function (data) {
            self.authors(data);
        });
    }

    function getAllGenres() {
        ajaxHelper(genresUri, 'GET').done(function (data) {
            self.genres(data);
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

    self.doSearch = function () {
        var queryString = "";
        queryString += formatUrl(self.selectedAuthor());
        queryString += formatUrl(self.selectedTitle());
        queryString += formatUrl(self.selectedGenre());
        queryString += formatUrl(self.selectedIsbn());

        ajaxHelper(booksUri + queryString, 'GET').done(function (data) {
            self.filteredBooks(data);
        });

    };

    self.getBookDetail = function (item) {
        ajaxHelper(booksUri + item.Id, 'GET').done(function (data) {
            self.bookDetail(data);
        });
    }

    self.loanBook = function (item) {
        ajaxHelper(booksUri + item.Id, 'GET').done(function (data) {
            console.log(data);
            self.bookDetail(data);
        });
    }

    self.loanBookSubmit = function (formElement) {
        var book = {
            BookId: self.newLoanBook.Book().Id,
            Date: self.newLoanBook.rawDate(),
            Name: self.newLoanBook.Name(),
            Comments: self.newLoanBook.Comments()
        };

        ajaxHelper(booksUri, 'POST', book).done(function (item) {
            self.books.push(item);
        });
    }

    getAllAuthors();
    getAllGenres();
};

ko.applyBindings(new ViewModel());