var ViewModel = function () {
    var self = this;
    var booksUri = '/api/books/';
    var authorsUri = '/api/authors/';
    var genresUri = '/api/genres/';
    var loansUri = '/api/loans/';
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
    self.loanedBook = ko.observable();

    self.newLoanBook = {
        Book : self.bookDetail,
        Title: ko.observable(),
        DateLoaned: ko.observable(moment(currentDate).format('DD-MM-YYYY')),
        Name: ko.observable(),
        Surname: ko.observable(),
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
            self.loanedBook = data;
            self.bookDetail(data);
        });
    }

    self.loanBookSubmit = function (formElement) {
        var loannee = {
            BookId: this.loanedBook.Id,
            DateLoaned: moment(self.newLoanBook.DateLoaned()).format('DD-MM-YYYY'),
            Name: self.newLoanBook.Name(),
            Surname: self.newLoanBook.Surname(),
            Comments: self.newLoanBook.Comments()
        };


        ajaxHelper(loansUri, 'POST', loannee).done(function (item) {
            self.books.push(item);
        });
    }

    getAllAuthors();
    getAllGenres();
};

ko.applyBindings(new ViewModel());