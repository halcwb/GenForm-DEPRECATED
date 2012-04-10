Ext.define('GenForm.test.usecase.NewProductTest', {
    describe: 'NewProductTest tests that',

    tests: function () {
        var me = this,
            queryHelper = Ext.create('GenForm.lib.util.QueryHelper'),
            messageChecker = Ext.create('GenForm.lib.util.MessageChecker'),
            domClicker = Ext.create('GenForm.lib.util.DomClicker'),
            message = '',
            product = 'dopamine Dynatra infusievloeistof 5 mL ampul',
            code = '123456',
            generic = 'dopamine',
            brand = 'Dynatra',
            shape = 'infusievloeistof',
            quantity = 5,
            unit = 'mL',
            productPackage = 'ampul',
            substance = 'dopamine',
            substanceUnit = 'mg';

        me.getNewProductButton = function () {
            return queryHelper.getButton('panel[title=Menu]','Nieuw Artikel');
        };

        me.clickNewProductButton = function () {
            queryHelper.clickButton(me.getNewProductButton());
        };

        me.clickSaveGenericButton = function () {
            queryHelper.clickButton(queryHelper.getButton('genericnamewindow', 'Opslaan'));
        };

        me.clickSaveBrandButton = function () {
            queryHelper.clickButton(queryHelper.getButton('brandnamewindow', 'Opslaan'));
        };

        me.clickSaveShapeButton = function () {
            queryHelper.clickButton(queryHelper.getButton('shapewindow', 'Opslaan'));
        };

        me.clickSaveUnitButton = function () {
            queryHelper.clickButton(queryHelper.getButton('unitwindow', 'Opslaan'));
        };

        me.clickSaveSubstanceUnitButton = function () {
            queryHelper.clickButton(queryHelper.getButton('substanceunitwindow', 'Opslaan'));
        };

        me.clickSavePackageButton = function () {
            queryHelper.clickButton(queryHelper.getButton('packagewindow', 'Opslaan'));
        };

        me.clickSaveSubstanceButton = function () {
            queryHelper.clickButton(queryHelper.getButton('substancewindow', 'Opslaan'));
        };

        me.clickSaveProductButton = function () {
            queryHelper.clickButton(queryHelper.getButton('productwindow', 'Opslaan'));
        };

        me.getNewGenericButton = function () {
            return queryHelper.getButton('panel[title=Menu]', 'Nieuw Generiek');
        };

        me.getProductWindow = function () {
            return Ext.ComponentQuery.query('productwindow')[0];
        };

        me.getGenericWindow = function () {
            return Ext.ComponentQuery.query('genericnamewindow')[0];
        };

        me.getBrandNameWindow = function () {
            return Ext.ComponentQuery.query('brandnamewindow')[0];
        };

        me.getShapeWindow = function () {
            return Ext.ComponentQuery.query('shapewindow')[0];
        };

        me.getUnitWindow = function () {
            return Ext.ComponentQuery.query('unitwindow')[0];
        };

        me.getSubstanceUnitWindow = function () {
            return Ext.ComponentQuery.query('substanceunitwindow')[0];
        };

        me.getPackageWindow = function () {
            return Ext.ComponentQuery.query('packagewindow')[0];
        };

        me.getSubstanceWindow = function () {
            return Ext.ComponentQuery.query('substancewindow')[0];
        };

        me.getProductSubstanceWindow = function () {
            return Ext.ComponentQuery.query('productsubstancewindow')[0];
        };

        me.getProductName = function () {
            return queryHelper.getFormTextField('productform', 'LabelName');
        };

        me.setProductName = function (value) {
            queryHelper.setFormField(me.getProductName(), value);
        };
        
        me.getProductCode = function () {
            return queryHelper.getFormTextField('productform', 'ProductCode');
        };

        me.setProductCode = function (name) {
            queryHelper.setFormField(me.getProductCode(), name);
        };

        me.getGenericName = function () {
            return queryHelper.getFormTextField('genericnamewindow', 'Name');
        };

        me.setGenericName = function (name) {
            queryHelper.setFormField(me.getGenericName(), name);
        };

        me.getBrandName = function () {
            return queryHelper.getFormTextField('brandnamewindow', 'Name');
        };

        me.setBrandName = function (name) {
            queryHelper.setFormField(me.getBrandName(), name);
        };

        me.getShapeName = function () {
            return queryHelper.getFormTextField('shapewindow', 'Name');
        };

        me.setShapeName = function (name) {
            queryHelper.setFormField(me.getShapeName(), name);
        };

        me.getUnitName = function () {
            return queryHelper.getFormTextField('unitwindow', 'Name');
        };

        me.setUnitName = function (name) {
            queryHelper.setFormField(me.getUnitName(), name);
        };

        me.getSubstanceUnitName = function () {
            return queryHelper.getFormTextField('substanceunitwindow', 'Name');
        };

        me.setSubstanceUnitName = function (name) {
            queryHelper.setFormField(me.getSubstanceUnitName(), name);
        };

        me.getPackageName = function () {
            return queryHelper.getFormTextField('packagewindow', 'Name');
        };

        me.setPackageName = function (name) {
            queryHelper.setFormField(me.getPackageName(), name);
        };

        me.getSubstanceName = function () {
            return queryHelper.getFormTextField('substancewindow', 'Name');
        };

        me.setSubstanceName = function (name) {
            queryHelper.setFormField(me.getSubstanceName(), name);
        };

        me.setGenericCombo = function (name) {
            queryHelper.setFormField(me.getGenericCombo(), name);
        };

        me.getGenericCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'GenericName');
        };

        me.setBrandCombo = function (name) {
            queryHelper.setFormField(me.getBrandCombo(), name);
        };

        me.getBrandCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'BrandName');
        };

        me.setShapeCombo = function (name) {
            queryHelper.setFormField(me.getShapeCombo(), name);
        };

        me.getShapeCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'Shape');
        };

        me.setUnitCombo = function (name) {
            queryHelper.setFormField(me.getUnitCombo(), name);
        };

        me.getUnitCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'Unit');
        };

        me.setSubstanceUnitCombo = function (name) {
            queryHelper.setFormField(me.getSubstanceUnitCombo(), name);
        };

        me.getSubstanceUnitCombo = function () {
            return queryHelper.getFormComboBox('productsubstancewindow', 'Unit');
        };

        me.setPackageCombo = function (name) {
            queryHelper.setFormField(me.getPackageCombo(), name);
        };

        me.setSubstanceCombo = function (name) {
            queryHelper.setFormField(me.getSubstanceCombo(), name);
        };

        me.getPackageCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'Package');
        };

        me.getSubstanceCombo = function () {
            return queryHelper.getFormComboBox('productsubstanceform', 'Substance');
        };

        me.getQuantity = function () {
            return queryHelper.getFormTextField('productwindow', 'Quantity');
        };

        me.setQuantity = function(quantity) {
            queryHelper.setFormField(me.getQuantity(), quantity);
        };

        me.checkMessage = function () {
            if (messageChecker.checkMessage(message)) {
                message = "";
                return true;
            } else {
                return false;
            }
        };

        me.clickButton = function (button) {
            if (!button) {
                Ext.Error.raise('Button is not defined');
            }
            domClicker.click(button.getEl().dom);
        };

        me.getAddButton = function (action) {
            return Ext.ComponentQuery.query('button[action=' + action + ']')[0];
        };

        me.clickAddGeneric = function () {
            var button = me.getAddButton('addGenericName');
            me.clickButton(button);
        };

        me.clickAddBrand = function () {
            var button = me.getAddButton('addBrandName');
            me.clickButton(button);
        };

        me.clickAddShape = function () {
            var button = me.getAddButton('addShape');
            me.clickButton(button);
        };

        me.clickAddUnit = function () {
            var button = me.getAddButton('addProductUnit');
            me.clickButton(button);
        };

        me.clickAddSubstanceUnit = function () {
            var button = me.getAddButton('addSubstanceUnit');
            me.clickButton(button);
        };

        me.clickAddSubstance = function () {
            var button = me.getAddButton('addSubstance');
            me.clickButton(button);
        };

        me.clickAddPackage = function () {
            var buton = me.getAddButton('addPackage');
            me.clickButton(buton);
        };

        me.getComboAddButton = function (name) {
            return me.getAddButtonInForm('productwindow', name);
        };

        me.getAddButtonInForm = function (formName, comboName) {
            return  Ext.ComponentQuery.query(formName + ' combobox[name=' + comboName + ']')[0].triggerWrap.dom.childNodes[2];
        };

        me.getAddSubstanceButton = function (name) {
            return me.getAddButtonInForm('productsubstancewindow', name)
        };

        me.clickAddProductSubstance = function () {
            var button = me.getAddButton('addProductSubstance');
            me.clickButton(button);
        };

        it('User sees an empty Product window', function () {
            me.clickNewProductButton();
            expect(me.getProductWindow()).toBeDefined();
        });

        it('The product window is empty', function () {
            expect(me.getProductWindow().isEmpty()).toBeTruthy();
        });

        it('User can enter a productname', function () {
            me.setProductName(product);

            expect(me.getProductName().value).toBe(product)
        });

        it('User can enter a product quantity', function () {
            me.setQuantity(quantity);
            expect(me.getQuantity().value).toBe(quantity)
        });

        it('User can open up a window to add a new generic', function () {
            me.clickAddGeneric();
            expect(me.getGenericWindow()).toBeDefined();
        });

        it('User can enter a name for the new generic', function () {
            me.setGenericName(generic);
            expect(me.getGenericName().value).toBe(generic);
        });

        it('A new generic with a valid name can be saved', function () {
            message = generic;
            me.clickSaveGenericButton();
            waitsFor(me.checkMessage, "response of generic save", GenForm.test.waitingTime);
        });

        it('The new generic can be set for the product', function () {
            me.setGenericCombo(generic);
            expect(me.getGenericCombo().value).toBe(generic);
        });

        it('User can open up a window to add a new Brand', function () {
            me.clickAddBrand();
            expect(me.getBrandNameWindow()).toBeDefined();
        });

        it('User can enter a name for the new brand', function (){
            me.setBrandName(brand);
            expect(me.getBrandName().value).toBe(brand);
        });

        it('User can save the new brand', function () {
            message = brand;
            me.clickSaveBrandButton();
            waitsFor(me.checkMessage, 'response of brand save', GenForm.test.waitingTime);
        });

        it('The new brand can be set for the product', function () {
            me.setBrandCombo(brand);
            expect(me.getBrandCombo().value).toBe(brand);
        });

        it('User can open up a window to add a Shape', function () {
            me.clickAddShape();
            expect(me.getShapeWindow()).toBeDefined();
        });

        it('User can enter a name for the new Shape', function () {
            me.setShapeName(shape);
            expect(me.getShapeName().value).toBe(shape);
        });

        it('User can save the new Shape', function () {
            message = shape;
            me.clickSaveShapeButton();
            waitsFor(me.checkMessage, 'response of shape save', GenForm.test.waitingTime);
        });

        it('The new Shape can be set for the product', function () {
            me.setShapeCombo(shape);
            expect(me.getShapeCombo().value).toBe(shape);
        });

        it('User can open up a window to add a Unit', function () {
            me.clickAddUnit();
            expect(me.getUnitWindow()).toBeDefined();
        });

        it('User can enter a name for unit', function () {
            me.setUnitName(unit);
            expect(me.getUnitName().value).toBe(unit);
        });

        it('User can save the new unit', function () {
            message = unit;
            me.clickSaveUnitButton();
            waitsFor(me.checkMessage, 'response of unit save', GenForm.test.waitingTime);
        });

        it('The new unit can be set for the product', function () {
            me.setUnitCombo(unit);
            expect(me.getUnitCombo().value).toBe(unit);
        });

        it('User can open a window to add a Package', function () {
            me.clickAddPackage();
            expect(me.getPackageWindow()).toBeDefined();
        });

        it('User can enter a Package name', function () {
            me.setPackageName(productPackage);
            expect(me.getPackageName().value).toBe(productPackage);
        });

        it('User can save the new Package', function () {
            message = productPackage;
            me.clickSavePackageButton();
            waitsFor(me.checkMessage, 'response of package save', GenForm.test.waitingTime);
        });

        it('The new package can be set for the product', function () {
            me.setPackageCombo(productPackage);
            expect(me.getPackageCombo().value).toBe(productPackage);
        });

        it('User can set the product code', function () {
            me.setProductCode(code);
            expect(me.getProductCode().value).toBe(code);
        });

        it('User can open up a window to add a product substance', function () {
            me.clickAddProductSubstance();
            expect(me.getProductSubstanceWindow()).toBeDefined();
        });

        it('The productsubstance window has a combobox to enter substance', function () {
            expect(me.getSubstanceCombo()).toBeDefined();
        });

        it('A substance can open a window to add a new substance', function () {
            me.clickAddSubstance();
            expect(me.getSubstanceWindow()).toBeDefined();
        });

        it('A new substance can be entered', function () {
            me.setSubstanceName(substance);
            expect(me.getSubstanceName().value).toBe(substance);
        });

        it('User can save the new Substance', function () {
            message = substance;
            me.clickSaveSubstanceButton();
            waitsFor(me.checkMessage, 'response of substance save', GenForm.test.waitingTime);
        });

        it('The new substance can be set for the productsubstance', function () {
            me.setSubstanceCombo(substance);
            expect(me.getSubstanceCombo().value).toBe(substance);
        });

        it('A user can open a window to add a new unit', function () {
            me.clickAddSubstanceUnit();
            expect(me.getSubstanceUnitWindow()).toBeDefined();
        });

        it('A new unit can be entered', function () {
            me.setSubstanceUnitName(substanceUnit);
            expect(me.getSubstanceUnitName().value).toBe(substanceUnit);
        });

        it('User can save the new substance unit', function () {
            message = substanceUnit;
            me.clickSaveSubstanceUnitButton();
            waitsFor(me.checkMessage, 'response of unit save', GenForm.test.waitingTime);
        });

        it('The new unit can be set for the productsubstance unit', function () {
            me.setSubstanceUnitCombo(substanceUnit);
            expect(me.getSubstanceUnitCombo().value).toBe(substanceUnit);
        });

        it('User can save the filled in Product', function () {
            message = product;
            me.clickSaveProductButton();
            waitsFor(me.checkMessage, 'response of product save', GenForm.test.waitingTime);
        });

        it('After saving the product, the new product window is closed', function () {
            var window = me.getProductWindow();
            expect(window).toBeUndefined();
        });

//        it('After saving the new product, all windows are closed', function () {
//            expect(Ext.ComponentQuery.query('window').length).toBe(0);
//        });
//        
    }
});

