Ext.define('GenForm.test.view.ProductSubstanceFormTests', {
    describe: 'ProductSubstanceFormShould',

    tests: function () {
        var me = this, form,
            queryHelper = Ext.create('GenForm.lib.util.QueryHelper'),
            formName = 'productsubstanceform';

        me.getProductSubstanceForm = function () {
            if (!form) form = Ext.create('GenForm.view.product.ProductSubstanceForm');
            return form;
        };

        me.getSubstanceComboBox = function () {
            return queryHelper.getFormComboBox(formName, 'SubstanceName');
        };

        me.getSubstanceUnitComboBox = function () {
            return queryHelper.getFormComboBox(formName, 'UnitName');
        };

        it('be defined', function () {
            expect(me.getProductSubstanceForm()).toBeDefined();
        });

        it('have a substance combobox', function () {
            expect(me.getSubstanceComboBox()).toBeDefined();
        });

        it('have a unit combobox', function () {
           expect(me.getSubstanceUnitComboBox()).toBeDefined();
        });

    }
});