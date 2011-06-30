if (GenForm === undefined) var GenForm = {};
if (!GenForm.test) GenForm.test = {};
GenForm.test.NewProductTest = {};

GenForm.test.NewProductTest.describe = 'NewProductTest tests that';
GenForm.test.NewProductTest.fn  = function () {
    var me = this;

    me.getTextField = function (fieldname) {
        return Ext.ComponentQuery.query('textfield[name=' + fieldname + ']')[0];
    };

    me.setTextField = function (textfield, value) {
        textfield.inputEl.dom.value = value;
        textfield.value = value;
        return true;
    };

    me.getProductWindow = function () {
        return Ext.ComponentQuery.query('productwindow')[0];
    };

    it('User sees an empty Product window', function () {
        expect(me.getProductWindow).toBeDefined();
    });

    it('The product window is empty', function () {
        expect(me.getProductWindow().isEmpty()).toBeTruthy();
    });

    it('The user can enter a productname', function () {
        var productName = 'dopamine (Dynatra) infusievloeistof 200 mg / 5 ml ampul', value;
        me.setTextField(me.getTextField('ProductName'), productName);
        
        expect(me.getTextField('ProductName').value).toBe(productName)
    });
};