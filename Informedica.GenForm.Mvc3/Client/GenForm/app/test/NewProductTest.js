Ext.define('GenForm.test.NewProductTest', {
    describe: 'NewProductTest tests that',

    fn: function () {
        var me = this, formQuery = Ext.create('GenForm.test.util.FormFieldQuery');

        me.getNewProductButton = function () {
            return Ext.ComponentQuery.query('panel[title=Menu] > buttongroup > button[text=Nieuw Artikel]')[0];
        };

        me.getNewGenericButton = function () {
            return Ext.ComponentQuery.query('panel[title=Menu] > buttongroup > button[text=Nieuw Generiek]')[0];
        };

        me.getFormField = function (form, fieldname) {
            return Ext.ComponentQuery.query(form + ' textfield[name=' + fieldname + ']')[0];
        };

        me.setFormField = function (formfield, value) {
            formfield.inputEl.dom.value = value;
            formfield.value = value;
            return true;
        };

        me.clickButton = function (button) {
            button.btnEl.dom.click();
        };

        me.getProductWindow = function () {
            return Ext.ComponentQuery.query('productwindow')[0];
        };

        me.getGenericWindow = function () {
            return Ext.ComponentQuery.query('genericwindow')[0];
        };

        it('User sees an empty Product window', function () {
            me.clickButton(me.getNewProductButton());
            expect(me.getProductWindow()).toBeDefined();
        });

        it('The product window is empty', function () {
            expect(me.getProductWindow().isEmpty()).toBeTruthy();
        });

        it('User can enter a productname', function () {
            var productName = 'dopamine (Dynatra) infusievloeistof 200 mg / 5 ml ampul', value;
            me.setFormField(me.getFormField('productform', 'ProductName'), productName);

            expect(me.getFormField('productform', 'ProductName').value).toBe(productName)
        });

        it('User can enter a product quantity', function () {
            var quantity = 5;
            me.setFormField(Ext.ComponentQuery.query('productform' + ' numberfield[name=Quantity]')[0], quantity);
            expect(Ext.ComponentQuery.query('productform' + ' numberfield[name=Quantity]')[0].value).toBe(quantity)
        });

        it('User can open up a window to add a new generic', function () {
            me.clickButton(me.getNewGenericButton());
            expect(me.getGenericWindow()).toBeDefined();
        });

        it('User can enter a name for the new generic', function () {
            var name = 'paracetamol';
            me.setFormField(me.getFormField('genericform', 'GenericName'), name);
            expect(me.getFormField('genericform', 'GenericName').value).toBe(name);
        });
    }
});

