Ext.define('GenForm.test.controller.handler.ProductHandlerTests', {
    describe: 'ProductHandlerShould',

    tests: function () {
        var handler;

        beforeEach(function () {
            if(!handler) handler = Ext.create('GenForm.controller.mixin.ProductHandler');
        });

        it('be defined', function () {
            expect(handler).toBeDefined();
        });

        it('should have a generic handler', function () {
            expect(handler.mixins.genericHandler).toBeDefined();
        });

        
    }
});