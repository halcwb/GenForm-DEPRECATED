Ext.define('GenForm.test.NewProductTest', {
    describe: 'NewProductTest tests that',

    fn: function () {
        console.log(this);
        var me = this,
            queryHelper = Ext.create('GenForm.test.util.QueryHelper'),
            messageChecker = Ext.create('GenForm.test.util.MessageChecker'),
            message = '',
            waitingTime = 500;

        me.getNewProductButton = function () {
            return queryHelper.getButton('panel[title=Menu]','Nieuw Artikel');
        };

        me.getNewGenericButton = function () {
            return queryHelper.getButton('panel[title=Menu]', 'Nieuw Generiek');
        };

        me.getProductWindow = function () {
            return Ext.ComponentQuery.query('productwindow')[0];
        };

        me.getGenericWindow = function () {
            return Ext.ComponentQuery.query('genericwindow')[0];
        };

        me.checkMessage = function () {
            return messageChecker.checkMessage('paracetamol');
        };

        it('User sees an empty Product window', function () {
            queryHelper.clickButton(me.getNewProductButton());
            expect(me.getProductWindow()).toBeDefined();
        });

        it('The product window is empty', function () {
            expect(me.getProductWindow().isEmpty()).toBeTruthy();
        });

        it('User can enter a productname', function () {
            var productName = 'dopamine (Dynatra) infusievloeistof 200 mg / 5 ml ampul';
            queryHelper.setFormField(queryHelper.getFormTextField('productform', 'ProductName'), productName);

            expect(queryHelper.getFormTextField('productform', 'ProductName').value).toBe(productName)
        });

        it('User can enter a product quantity', function () {
            var quantity = 5;
            queryHelper.setFormField(queryHelper.getFormNumberField('productform', 'Quantity'), quantity);
            expect(queryHelper.getFormNumberField('productform', 'Quantity').value).toBe(quantity)
        });

        it('User can open up a window to add a new generic', function () {
            queryHelper.clickButton(me.getNewGenericButton());
            expect(me.getGenericWindow()).toBeDefined();
        });

        it('User can enter a name for the new generic', function () {
            var name = 'paracetamol';
            queryHelper.setFormField(queryHelper.getFormTextField('genericform', 'GenericName'), name);
            expect(queryHelper.getFormTextField('genericform', 'GenericName').value).toBe(name);
        });

        it('A new generic with a valid name can be saved', function () {
            //var me = this;
            queryHelper.clickButton(queryHelper.getButton('genericwindow', 'Opslaan'));

            message = 'paracetamol';
            waitsFor(me.checkMessage, "response of generic save", waitingTime);
        });
    }
});

